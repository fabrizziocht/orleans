using System;
using Orleans;
using System.Collections;
using System.Collections.Generic;
using Orleans.Runtime;
using System.Linq;

namespace UnitTests.StorageTests.Relational.TestDataSets
{
    /// <summary>
    /// A set of simple test data set wit and without extension keys.
    /// </summary>
    /// <typeparam name="TGrainKey">The grain type (integer, guid or string)</typeparam>.
    internal sealed class StorageDataSetPlain<TGrainKey>: IEnumerable<object[]>
    {
        private const int NumCases = 3;

        /// <summary>
        /// The symbol set this data set uses.
        /// </summary>
        private static SymbolSet Symbols { get; } = new SymbolSet(SymbolSet.Latin1);

        /// <summary>
        /// The length of random string drawn form <see cref="Symbols"/>.
        /// </summary>
        private const long StringLength = 15L;

        internal record TestData(string GrainType, Func<IInternalGrainFactory, GrainReference> GrainGetter, GrainState<TestState1> GrainState);

        public static TestData GetTestData(int testNum) => testNum switch
        {
            0 => new TestData(
                GrainTypeGenerator.GetGrainType<TGrainKey>(),
                grainFactory => RandomUtilities.GetRandomGrainReference<TGrainKey>(grainFactory, extensionKey: false),
                new GrainState<TestState1> { State = new TestState1 { A = RandomUtilities.GetRandomCharacters(Symbols, StringLength), B = 1, C = 4 } }),
            1 => new TestData(
                GrainTypeGenerator.GetGrainType<TGrainKey>(),
                grainFactory => RandomUtilities.GetRandomGrainReference<TGrainKey>(grainFactory, true),
                new GrainState<TestState1> { State = new TestState1 { A = RandomUtilities.GetRandomCharacters(Symbols, StringLength), B = 2, C = 5 } }),
            2 => new TestData(
                GrainTypeGenerator.GetGrainType<TGrainKey>(),
                grainFactory => RandomUtilities.GetRandomGrainReference<TGrainKey>(grainFactory, true),
                new GrainState<TestState1> { State = new TestState1 { A = RandomUtilities.GetRandomCharacters(Symbols, StringLength), B = 3, C = 6 } }),
            _ => throw new IndexOutOfRangeException(),

        };

        public IEnumerator<object[]> GetEnumerator() => Enumerable.Range(0, NumCases).Select(n => new object[] { n }).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
