﻿using NeuralNetwork.Enums;
using NeuralNetwork.Interfaces;
using NeuralNetwork.Utils;
using System;

namespace NeuralNetwork.Neurons
{
    internal abstract class Neuron : INeuron
    {
        protected float _error = 0;
        protected float _value = 0;
        protected float _lastValue = 0;

#if DEBUG
        public Guid ID { get; } = Guid.NewGuid();
#endif
        public IActivationFunction Function { get; private set; }
        public ISynapseCollection<ISynapse> Inputs { get; protected set; }
        public ISynapseCollection<ISynapse> Outputs { get; protected set; }

        public Neuron(ActivationTypes activationType)
        {
            this.Function = NetworkUtils.GetActivation(activationType);
        }

        public abstract void Backpropagate(float errorSignal, float eWeightedSignal = 0, Action<float> updateWeight = null);

        public abstract void Propagate(float value);

        protected void AccumulateError(float errorSignal, float eWeightedSignal = 0, Action<float> updateWeight = null)
        {
            _error += eWeightedSignal; //Sum
            updateWeight(errorSignal * _lastValue);
            Outputs.AccountSignal();
        }
    }
}
