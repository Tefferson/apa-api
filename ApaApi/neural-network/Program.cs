using Encog.Engine.Network.Activation;
using Encog.ML.Data;
using Encog.ML.Data.Basic;
using Encog.Neural.Networks;
using Encog.Neural.Networks.Layers;
using Encog.Neural.Networks.Training.Propagation.Resilient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace neural_network
{
    class Program
    {
        static void Main(string[] args)
        {
            var apann = new ApaNN();
            apann.Train();

            var network = apann.LoadModel();
            apann.Execute(network);

            Console.Read();
            Console.Read();
        }
    }

    class ApaNN
    {
        private readonly IMLDataSet Dataset;
        private readonly string Path;

        public ApaNN()
        {
            Dataset = LoadDataSet();
            Path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), $"model.txt");
        }

        public void Save(BasicNetwork network)
        {
            using (var fs = new FileStream(Path, FileMode.OpenOrCreate))
            {
                new PersistBasicNetwork().Save(fs, network);
            }
        }

        public BasicNetwork LoadModel()
        {
            var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "model.txt");
            using (var fs = new FileStream(path, FileMode.Open))
            {
                return (BasicNetwork)new PersistBasicNetwork().Read(fs);
            }
        }

        public void Execute(BasicNetwork network)
        {
            var showLess = true;
            Console.WriteLine("Neural Network Results:");
            foreach (var pair in Dataset)
            {
                var output = network.Compute(pair.Input);

                var actual = string.Empty;
                for (int i = 0; i < output.Count; actual += output[i++] + ", ") ;

                var ideal = string.Empty;
                for (int i = 0; i < pair.Ideal.Count; ideal += pair.Ideal[i++] + ", ") ;

                var input = string.Empty;
                for (int i = 0; i < pair.Input.Count; input += pair.Input[i++] + ", ") ;

                if (showLess)
                {
                    Console.WriteLine($"actual={actual} ideal={ideal}");
                }
                else
                {
                    Console.WriteLine($"input={input} actual={actual} ideal={ideal}");
                }
            }
        }

        public BasicMLDataSet LoadDataSet()
        {
            var path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "dataset", "dataset.txt");
            var lines = File.ReadAllLines(path);

            var input = new List<double[]>();
            var ideal = new List<double[]>();

            var outputs = int.Parse(lines.First());
            //first line is the header, skipping it
            foreach (var line in lines.Skip(1))
            {
                var values = new List<string>(line.Split(' '));
                var newInput = values.Skip(1).Select(v => double.Parse(v) / 1024d).ToArray();
                var newIdeal = new double[outputs];
                newIdeal[int.Parse(values.First())] = 1d;

                input.Add(newInput);
                ideal.Add(newIdeal);
            }

            return new BasicMLDataSet(input.ToArray(), ideal.ToArray());
        }

        public void Train()
        {
            var network = new BasicNetwork();
            network.AddLayer(new BasicLayer(null, true, Dataset.InputSize));
            for (int i = 0; i <= Dataset.IdealSize; i++)
                network.AddLayer(new BasicLayer(new ActivationSigmoid(), true, Dataset.InputSize));
            network.AddLayer(new BasicLayer(new ActivationSigmoid(), false, Dataset.IdealSize));
            network.Structure.FinalizeStructure();
            network.Reset();

            var train = new ResilientPropagation(network, Dataset);
            var epoch = 1;

            do
            {
                train.Iteration();
                Console.WriteLine($"Epoch: # {epoch}; Error: {train.Error};");
                epoch++;
            } while (train.Error > 0.1);

            Save(network);
        }
    }
}
