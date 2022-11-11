// Copyright (c) Microsoft Corporation.  All Rights Reserved.  See License.txt in the project root for license information.

// ----------------------------------------------------------------------------
// F# fluent extensions (Fluent.fs)
// ----------------------------------------------------------------------------
namespace FSharp.Core.Fluent

open System
open System.IO
open System.Runtime.CompilerServices
open Microsoft.FSharp.NativeInterop

#nowarn "9" // nativeptr

[<assembly: Extension>]
do()



/// <summary>Fluent extension operations on sequences.</summary>
[<AutoOpen>]
module SeqExtensions =

    type System.Collections.Generic.IEnumerable<'T> with

        /// <summary>Returns a new sequence that contains all pairings of elements from the first and second sequences.</summary>
        ///
        /// <param name="source2">The second input sequence.</param>
        /// <exception cref="System.ArgumentException">Thrown when either of the input sequences is null.</exception>
        ///
        /// <returns>The resulting sequence of pairs.</returns>
        member inline source.allPairs source2 = Seq.allPairs source source2

        /// <summary>Return a new sequence with a new item inserted before the given index.</summary>
        ///
        /// <param name="index">The index where the item should be inserted.</param>
        /// <param name="value">The value to insert.</param>
        /// <exception cref="System.ArgumentException">Thrown when index is below 0 or greater than source.Length.</exception>
        ///
        /// <returns>The result sequence.</returns>
        member inline source.insertAt(index, value) = Seq.insertAt index value source

        /// <summary>Return a new sequence with new items inserted before the given index.</summary>
        ///
        /// <param name="index">The index where the items should be inserted.</param>
        /// <param name="values">The values to insert.</param>
        /// <exception cref="System.ArgumentException">Thrown when index is below 0 or greater than source.Length.</exception>
        ///
        /// <returns>The result sequence.</returns>
        member inline source.insertManyAt(index, values) = Seq.insertManyAt index values source

        /// <summary>Return a new sequence with the item at a given index removed.</summary>
        ///
        /// <param name="index">The index of the item to be removed.</param>
        /// <exception cref="System.ArgumentException">Thrown when index is outside 0..source.Length - 1</exception>
        ///
        /// <returns>The result sequence.</returns>
        member inline source.removeAt index = Seq.removeAt index source

        /// <summary>Return a new sequence with the number of items starting at a given index removed.</summary>
        ///
        /// <param name="index">The index of the item to be removed.</param>
        /// <param name="count">The number of items to remove.</param>
        /// <exception cref="System.ArgumentException">Thrown when index is outside 0..source.Length - count</exception>
        ///
        /// <returns>The result sequence.</returns>
        member inline source.removeManyAt(index, count) = Seq.removeManyAt index count source

        /// <summary>Returns the only element of the sequence or <c>None</c> if it is empty or contains more than one element.</summary>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        ///
        /// <returns>The only element of the sequence or None.</returns>
        member inline source.tryExactlyOne() = Seq.tryExactlyOne source

        /// <summary>Return a new sequence with the item at a given index set to the new value.</summary>
        ///
        /// <param name="index">The index of the item to be replaced.</param>
        /// <param name="value">The new value.</param>
        ///
        /// <exception cref="System.ArgumentException">Thrown when index is outside 0..source.Length - 1</exception>
        ///
        /// <returns>The result sequence.</returns>
        member inline source.updateAt(index, value) = Seq.updateAt index value source

        /// <summary>Wraps the two given enumerations as a single concatenated
        /// enumeration.</summary>
        ///
        /// <remarks>The returned sequence may be passed between threads safely. However,
        /// individual IEnumerator values generated from the returned sequence should not be accessed
        /// concurrently.</remarks>
        ///
        /// <param name="source2">The second sequence.</param>
        ///
        /// <returns>The result sequence.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when either of the two provided sequences is
        /// null.</exception>
        member inline source.append (source2:seq<'T>) = Seq.append source source2


        /// <summary>Returns a sequence that corresponds to a cached version of the input sequence.
        /// This result sequence will have the same elements as the input sequence. The result
        /// can be enumerated multiple times. The input sequence will be enumerated at most
        /// once and only as far as is necessary.  Caching a sequence is typically useful when repeatedly
        /// evaluating items in the original sequence is computationally expensive or if
        /// iterating the sequence causes side-effects that the user does not want to be
        /// repeated multiple times.
        ///
        /// Enumeration of the result sequence is thread safe in the sense that multiple independent IEnumerator
        /// values may be used simultaneously from different threads (accesses to
        /// the internal lookaside table are thread safe). Each individual IEnumerator
        /// is not typically thread safe and should not be accessed concurrently.</summary>
        ///
        /// <remarks>Once enumeration of the input sequence has started,
        /// it's enumerator will be kept live by this object until the enumeration has completed.
        /// At that point, the enumerator will be disposed.
        ///
        /// The enumerator may be disposed and underlying cache storage released by
        /// converting the returned sequence object to type IDisposable, and calling the Dispose method
        /// on this object. The sequence object may then be re-enumerated and a fresh enumerator will
        /// be used.</remarks>
        ///
        /// <returns>The result sequence.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        member inline source.cache() = Seq.cache source


        /// <summary>Applies the given function to each element of the list. Return
        /// the list comprised of the results "x" for each element where
        /// the function returns Some(x).</summary>
        ///
        /// <remarks>The returned sequence may be passed between threads safely. However,
        /// individual IEnumerator values generated from the returned sequence should not
        /// be accessed concurrently.</remarks>
        ///
        /// <param name="chooser">A function to transform items of type T into options of type U.</param>
        ///
        /// <returns>The result sequence.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        member inline source.choose (chooser:('T -> 'U option)) = Seq.choose chooser source

        /// <summary>Divides the input sequence into chunks of size at most <c>chunkSize</c>.</summary>
        ///
        /// <param name="chunkSize">The maximum size of each chunk.</param>
        ///
        /// <returns>The sequence divided into chunks.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown when <c>chunkSize</c> is not positive.</exception>
        member inline source.chunkBySize (chunkSize:int) = Seq.chunkBySize chunkSize source

        /// <summary>Applies the given function to each element of the sequence and concatenates all the
        /// results.</summary>
        ///
        /// <remarks>Remember sequence is lazy, effects are delayed until it is enumerated.</remarks>
        ///
        /// <param name="mapping">A function to transform elements of the input sequence into the sequences
        /// that will then be concatenated.</param>
        ///
        /// <returns>The result sequence.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        member inline source.collect (mapping:('T -> ('Collection :> seq<'U>))) = Seq.collect mapping source

        /// <summary>Compares two sequences using the given comparison function, element by element.
        /// Returns the first non-zero result from the comparison function.  If the end of a sequence
        /// is reached it returns a -1 if the first sequence is shorter and a 1 if the second sequence
        /// is shorter.</summary>
        ///
        /// <param name="comparer">A function that takes an element from each sequence and returns an int.
        /// If it evaluates to a non-zero value iteration is stopped and that value is returned.</param>
        /// <param name="source2">The second input sequence.</param>
        ///
        /// <returns>The first non-zero value from the comparison function.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when either of the input sequences
        /// is null.</exception>
        member inline source.compareWith (comparer:('T -> 'T -> int), source2:seq<'T>) =  Seq.compareWith comparer source source2

        /// <summary>Applies a key-generating function to each element of a sequence and returns a sequence yielding unique
        /// keys and their number of occurrences in the original sequence.</summary>
        ///
        /// <remarks>Note that this function returns a sequence that digests the whole initial sequence as soon as
        /// that sequence is iterated. As a result this function should not be used with
        /// large or infinite sequences. The function makes no assumption on the ordering of the original
        /// sequence.</remarks>
        ///
        /// <param name="projection">A function transforming each item of the input sequence into a key to be
        /// compared against the others.</param>
        ///
        /// <returns>The result sequence.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        member inline source.countBy(projection:('T -> 'Key)) = Seq.countBy projection source

        /// <summary>Returns a sequence that contains no duplicate entries according to the
        /// generic hash and equality comparisons on the keys returned by the given key-generating function.
        /// If an element occurs multiple times in the sequence then the later occurrences are discarded.</summary>
        ///
        /// <param name="projection">A function transforming the sequence items into comparable keys.</param>
        ///
        /// <returns>The result sequence.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        member inline source.distinctBy (projection:('T -> 'Key)) = Seq.distinctBy projection source

        /// <summary>Splits the input sequence into at most <c>count</c> chunks.</summary>
        ///
        /// <remarks>This function returns a sequence that digests the whole initial sequence as soon as that
        /// sequence is iterated. As a result this function should not be used with large or infinite sequences.</remarks>
        ///
        /// <param name="count">The maximum number of chunks.</param>
        ///
        /// <returns>The sequence split into chunks.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown when <c>count</c> is not positive.</exception>
        member inline source.splitInto (count:int) = Seq.splitInto count source

        /// <summary>Tests if any element of the sequence satisfies the given predicate.</summary>
        ///
        /// <remarks>The predicate is applied to the elements of the input sequence. If any application
        /// returns true then the overall result is true and no further elements are tested.
        /// Otherwise, false is returned.</remarks>
        ///
        /// <param name="predicate">A function to test each item of the input sequence.</param>
        ///
        /// <returns>True if any result from the predicate is true; false otherwise.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        member inline source.exists predicate = Seq.exists predicate source


        /// <summary>Returns a new collection containing only the elements of the collection
        /// for which the given predicate returns "true". This is a synonym for Seq.where.</summary>
        ///
        /// <remarks>The returned sequence may be passed between threads safely. However,
        /// individual IEnumerator values generated from the returned sequence should not be accessed concurrently.
        ///
        /// Remember sequence is lazy, effects are delayed until it is enumerated.</remarks>
        ///
        /// <param name="predicate">A function to test whether each item in the input sequence should be included in the output.</param>
        ///
        /// <returns>The result sequence.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        member inline source.filter (predicate:('T -> bool)) = Seq.filter predicate source

        /// <summary>Returns a new collection containing only the elements of the collection
        /// for which the given predicate returns "true".</summary>
        ///
        /// <remarks>The returned sequence may be passed between threads safely. However,
        /// individual IEnumerator values generated from the returned sequence should not be accessed concurrently.
        ///
        /// Remember sequence is lazy, effects are delayed until it is enumerated.
        ///
        /// A synonym for Seq.filter.</remarks>
        ///
        /// <param name="predicate">A function to test whether each item in the input sequence should be included in the output.</param>
        ///
        /// <returns>The result sequence.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        member inline source.where (predicate:('T -> bool)) = Seq.where predicate source

        /// <summary>Returns the first element for which the given function returns <c>true</c>.</summary>
        ///
        /// <param name="predicate">A function to test whether an item in the sequence should be returned.</param>
        ///
        /// <returns>The first element for which the predicate returns <c>true</c>.</returns>
        ///
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">Thrown if no element returns true when
        /// evaluated by the predicate</exception>
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null</exception>
        member inline source.find (predicate:('T -> bool)) = Seq.find predicate source

        /// <summary>Returns the last element for which the given function returns <c>true</c>.</summary>
        /// <remarks>This function digests the whole initial sequence as soon as it is called. As a
        /// result this function should not be used with large or infinite sequences.</remarks>
        /// <param name="predicate">A function to test whether an item in the sequence should be returned.</param>
        /// <returns>The last element for which the predicate returns <c>true</c>.</returns>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">Thrown if no element returns true when
        /// evaluated by the predicate</exception>
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null</exception>
        member inline source.findBack (predicate:('T -> bool)) = Seq.findBack predicate source

        /// <summary>Returns the index of the first element for which the given function returns <c>true</c>.</summary>
        ///
        /// <param name="predicate">A function to test whether the index of a particular element should be returned.</param>
        ///
        /// <returns>The index of the first element for which the predicate returns <c>true</c>.</returns>
        ///
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">Thrown if no element returns true when
        /// evaluated by the predicate</exception>
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null</exception>
        member inline source.findIndex (predicate:('T -> bool)) = Seq.findIndex predicate source

        /// <summary>Returns the index of the last element for which the given function returns <c>true</c>.</summary>
        ///
        /// <remarks>This function digests the whole initial sequence as soon as it is called. As a
        /// result this function should not be used with large or infinite sequences.</remarks>
        ///
        /// <param name="predicate">A function to test whether the index of a particular element should be returned.</param>
        ///
        /// <returns>The index of the last element for which the predicate returns <c>true</c>.</returns>
        ///
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">Thrown if no element returns true when
        /// evaluated by the predicate</exception>
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null</exception>
        member inline source.findIndexBack (predicate:('T -> bool)) = Seq.findIndexBack predicate source

        /// <summary>Applies a function to each element of the collection, threading an accumulator argument
        /// through the computation. If the input function is <c>f</c> and the elements are <c>i0...iN</c>
        /// then computes <c>f (... (f s i0)...) iN</c></summary>
        ///
        /// <param name="folder">A function that updates the state with each element from the sequence.</param>
        /// <param name="state">The initial state.</param>
        ///
        /// <returns>The state object after the folding function is applied to each element of the sequence.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        member inline source.fold (state:'State, folder:('State -> 'T -> 'State)) = Seq.fold folder state source

        /// <summary>Applies a function to each element of the collection, starting from the end, threading an accumulator argument
        /// through the computation. If the input function is <c>f</c> and the elements are <c>i0...iN</c>
        /// then computes <c>f i0 (... (f iN s)...)</c></summary>
        ///
        /// <param name="folder">The function to update the state given the input elements.</param>
        /// <param name="state">The initial state.</param>
        ///
        /// <returns>The state object after the folding function is applied to each element of the sequence.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        member inline source.foldBack  (folder:('T -> 'State -> 'State), state: 'State) = Seq.foldBack folder source state

        /// <summary>Tests if all elements of the sequence satisfy the given predicate.</summary>
        ///
        /// <remarks>The predicate is applied to the elements of the input sequence. If any application
        /// returns false then the overall result is false and no further elements are tested.
        /// Otherwise, true is returned.</remarks>
        ///
        /// <param name="predicate">A function to test an element of the input sequence.</param>
        ///
        /// <returns>True if every element of the sequence satisfies the predicate; false otherwise.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        member inline source.forall (predicate:('T -> bool)) = Seq.forall predicate source

        /// <summary>Applies a key-generating function to each element of a sequence and yields a sequence of
        /// unique keys. Each unique key contains a sequence of all elements that match
        /// to this key.</summary>
        ///
        /// <remarks>This function returns a sequence that digests the whole initial sequence as soon as
        /// that sequence is iterated. As a result this function should not be used with
        /// large or infinite sequences. The function makes no assumption on the ordering of the original
        /// sequence.</remarks>
        ///
        /// <param name="projection">A function that transforms an element of the sequence into a comparable key.</param>
        ///
        /// <returns>The result sequence.</returns>
        member inline source.groupBy (projection:('T -> 'Key)) = Seq.groupBy projection source

        /// <summary>Returns the first element of the sequence.</summary>
        ///
        /// <returns>The first element of the sequence.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown when the input does not have any elements.</exception>
        member inline source.head() = Seq.head source

        /// <summary>Returns the first element of the sequence, or None if the sequence is empty.</summary>
        ///
        ///
        /// <returns>The first element of the sequence or None.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        member inline source.tryHead() = Seq.tryHead source

        /// <summary>Returns the last element of the sequence.</summary>
        ///
        /// <returns>The last element of the sequence.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown when the input does not have any elements.</exception>
        member inline source.last() = Seq.last source

        /// <summary>Returns the last element of the sequence.
        /// Return <c>None</c> if no such element exists.</summary>
        ///
        /// <returns>The last element of the sequence or None.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        member inline source.tryLast() = Seq.tryLast source

        /// <summary>Returns the only element of the sequence.</summary>
        ///
        /// <returns>The only element of the sequence.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown when the input does not have precisely one element.</exception>
        member inline source.exactlyOne() = Seq.exactlyOne source

        /// <summary>Returns true if the sequence contains no elements, false otherwise.</summary>
        ///
        ///
        /// <returns>True if the sequence is empty; false otherwise.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        member inline source.IsEmpty = Seq.isEmpty source

        /// <summary>Builds a new collection whose elements are the corresponding elements of the input collection
        /// paired with the integer index (from 0) of each element.</summary>
        ///
        /// <returns>The result sequence.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        member inline source.indexed() = Seq.indexed source

        /// <summary>Computes the element at the specified index in the collection.</summary>
        /// <param name="index">The index of the element to retrieve.</param>
        ///
        /// <returns>The element at the specified index of the sequence.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown when the index is negative or the input sequence does not contain enough elements.</exception>
        member inline source.Item(index:int) = Seq.item index source

        /// <summary>Applies the given function to each element of the collection.</summary>
        ///
        /// <param name="action">A function to apply to each element of the sequence.</param>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        member inline source.iter (action:('T -> unit)) = Seq.iter action source

        /// <summary>Applies the given function to each element of the collection. The integer passed to the
        /// function indicates the index of element.</summary>
        ///
        /// <param name="action">A function to apply to each element of the sequence that can also access the current index.</param>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        member inline source.iteri (action:(int -> 'T -> unit)) = Seq.iteri action source


        /// <summary>Returns the length of the sequence</summary>
        ///
        ///
        /// <returns>The length of the sequence.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        member inline source.length = Seq.length source

        /// <summary>Builds a new collection whose elements are the results of applying the given function
        /// to each of the elements of the collection.  The given function will be applied
        /// as elements are demanded using the <c>MoveNext</c> method on enumerators retrieved from the
        /// object.</summary>
        ///
        /// <remarks>The returned sequence may be passed between threads safely. However,
        /// individual IEnumerator values generated from the returned sequence should not be accessed concurrently.</remarks>
        ///
        /// <param name="mapping">A function to transform items from the input sequence.</param>
        ///
        /// <returns>The result sequence.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        member inline source.map (mapping:('T -> 'U)) = Seq.map mapping source


        /// <summary>Builds a new collection whose elements are the results of applying the given function
        /// to each of the elements of the collection. The integer index passed to the
        /// function indicates the index (from 0) of element being transformed.</summary>
        ///
        /// <param name="mapping">A function to transform items from the input sequence that also supplies the current index.</param>
        ///
        /// <returns>The result sequence.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        member inline source.mapi (mapping:(int -> 'T -> 'U)) = Seq.mapi mapping source

        /// <summary>Returns a sequence of each element in the input sequence and its predecessor, with the
        /// exception of the first element which is only returned as the predecessor of the second element.</summary>
        ///
        ///
        /// <returns>The result sequence.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        member inline source.pairwise() = Seq.pairwise source

        /// <summary>Combines map and fold. Builds a new collection whose elements are the results of applying the given function
        /// to each of the elements of the collection. The function is also used to accumulate a final value.</summary>
        ///
        /// <remarks>This function digests the whole initial sequence as soon as it is called. As a result this function should
        /// not be used with large or infinite sequences.</remarks>
        ///
        /// <param name="mapping">The function to transform elements from the input collection and accumulate the final value.</param>
        /// <param name="state">The initial state.</param>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input collection is null.</exception>
        ///
        /// <returns>The collection of transformed elements, and the final accumulated value.</returns>
        member inline source.mapFold<'T,'State,'Result>(state:'State, mapping:('State -> 'T -> 'Result * 'State)) = Seq.mapFold mapping state source

        /// <summary>Combines map and foldBack. Builds a new collection whose elements are the results of applying the given function
        /// to each of the elements of the collection. The function is also used to accumulate a final value.</summary>
        ///
        /// <remarks>This function digests the whole initial sequence as soon as it is called. As a result this function should
        /// not be used with large or infinite sequences.</remarks>
        ///
        /// <param name="mapping">The function to transform elements from the input collection and accumulate the final value.</param>
        /// <param name="state">The initial state.</param>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input collection is null.</exception>
        ///
        /// <returns>The collection of transformed elements, and the final accumulated value.</returns>
        member inline source.mapFoldBack(mapping:('T -> 'State -> 'Result * 'State), state:'State) = Seq.mapFoldBack mapping source state

        /// <summary>Returns a sequence with all elements permuted according to the
        /// specified permutation.</summary>
        ///
        /// <remarks>Note that this function returns a sequence that digests the whole initial sequence as soon as
        /// that sequence is iterated. As a result this function should not be used with
        /// large or infinite sequences.</remarks>
        ///
        /// <param name="indexMap">The function that maps input indices to output indices.</param>
        ///
        /// <returns>The result sequence.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown when indexMap does not produce a valid permutation.</exception>
        member inline source.permute (indexMap:(int -> int)) = Seq.permute indexMap source

        /// <summary>Applies the given function to successive elements, returning the first
        /// <c>x</c> where the function returns "Some(x)".</summary>
        ///
        /// <param name="chooser">A function to transform each item of the input sequence into an option of the output type.</param>
        ///
        /// <returns>The selected element.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">Thrown when every item of the sequence
        /// evaluates to <c>None</c> when the given function is applied.</exception>
        member inline source.pick (chooser:('T -> 'U option)) = Seq.pick chooser source

        /// <summary>Builds a new sequence object that delegates to the given sequence object. This ensures
        /// the original sequence cannot be rediscovered and mutated by a type cast. For example,
        /// if given an array the returned sequence will return the elements of the array, but
        /// you cannot cast the returned sequence object to an array.</summary>
        ///
        /// <returns>The result sequence.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        member inline source.readonly() = Seq.readonly source

        /// <summary>Applies a function to each element of the sequence, threading an accumulator argument
        /// through the computation. Begin by applying the function to the first two elements.
        /// Then feed this result into the function along with the third element and so on.
        /// Return the final result.</summary>
        ///
        /// <param name="reduction">A function that takes in the current accumulated result and the next
        /// element of the sequence to produce the next accumulated result.</param>
        ///
        /// <returns>The final result of the reduction function.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown when the input sequence is empty.</exception>
        member inline source.reduce (reduction:('T -> 'T -> 'T)) = Seq.reduce reduction source

        /// <summary>Applies a function to each element of the sequence, starting from the end, threading an accumulator argument
        /// through the computation. If the input function is <c>f</c> and the elements are <c>i0...iN</c>
        /// then computes <c>f i0 (...(f iN-1 iN))</c>.</summary>
        ///
        /// <param name="reduction">A function that takes in the next-to-last element of the sequence and the
        /// current accumulated result to produce the next accumulated result.</param>
        ///
        /// <returns>The final result of the reductions.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown when the input sequence is empty.</exception>
        member inline source.reduceBack (reduction:('T -> 'T -> 'T)) = Seq.reduceBack reduction source

        /// <summary>Returns a new sequence with the elements in reverse order.</summary>
        /// <returns>The reversed sequence.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        member inline source.reverse () = Seq.rev source

        /// <summary>Like fold, but computes on-demand and returns the sequence of intermediary and final results.</summary>
        ///
        /// <param name="folder">A function that updates the state with each element from the sequence.</param>
        /// <param name="state">The initial state.</param>
        ///
        /// <returns>The resulting sequence of computed states.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        member inline source.scan(state:'State, folder:('State -> 'T -> 'State)) = Seq.scan folder state source

        /// <summary>Like <c>foldBack</c>, but returns the sequence of intermediary and final results.</summary>
        ///
        /// <remarks>This function returns a sequence that digests the whole initial sequence as soon as that
        /// sequence is iterated. As a result this function should not be used with large or infinite sequences.
        /// </remarks>
        ///
        /// <param name="folder">A function that updates the state with each element from the sequence.</param>
        /// <param name="state">The initial state.</param>
        ///
        /// <returns>The resulting sequence of computed states.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        member inline source.scanBack(folder:('T -> 'State -> 'State), state:'State) = Seq.scanBack folder source state

        /// <summary>Returns a sequence that skips N elements of the underlying sequence and then yields the
        /// remaining elements of the sequence.</summary>
        ///
        /// <param name="count">The number of items to skip.</param>
        ///
        /// <returns>The result sequence.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        /// <exception cref="System.InvalidOperationException">Thrown when count exceeds the number of elements
        /// in the sequence.</exception>
        member inline source.skip(count:int) = Seq.skip count source

        /// <summary>Returns a sequence that, when iterated, skips elements of the underlying sequence while the
        /// given predicate returns <c>true</c>, and then yields the remaining elements of the sequence.</summary>
        ///
        /// <param name="predicate">A function that evaluates an element of the sequence to a boolean value.</param>
        ///
        /// <returns>The result sequence.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        member inline source.skipWhile(predicate:('T -> bool)) = Seq.skipWhile predicate source

        /// <summary>Yields a sequence ordered using the given comparison function.</summary>
        /// <remarks>This function returns a sequence that digests the whole initial sequence as soon as
        /// that sequence is iterated. As a result this function should not be used with
        /// large or infinite sequences. The function makes no assumption on the ordering of the original
        /// sequence.
        ///
        /// This is a stable sort, that is the original order of equal elements is preserved.</remarks>
        /// <param name="comparer">The function to compare the collection elements.</param>
        /// <returns>The result sequence.</returns>
        member inline source.sortWith(comparer:('T -> 'T -> int)) = Seq.sortWith comparer source

        /// <summary>Applies a key-generating function to each element of a sequence and yield a sequence ordered
        /// by keys.  The keys are compared using generic comparison as implemented by <c>Operators.compare</c>.</summary>
        ///
        /// <remarks>This function returns a sequence that digests the whole initial sequence as soon as
        /// that sequence is iterated. As a result this function should not be used with
        /// large or infinite sequences. The function makes no assumption on the ordering of the original
        /// sequence.
        ///
        /// This is a stable sort, that is the original order of equal elements is preserved.</remarks>
        ///
        /// <param name="projection">A function to transform items of the input sequence into comparable keys.</param>
        ///
        /// <returns>The result sequence.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        member inline source.sortBy(projection:('T -> 'Key)) = Seq.sortBy projection source

        /// <summary>Applies a key-generating function to each element of a sequence and yield a sequence ordered
        /// descending by keys.  The keys are compared using generic comparison as implemented by <c>Operators.compare</c>.</summary>
        ///
        /// <remarks>This function returns a sequence that digests the whole initial sequence as soon as
        /// that sequence is iterated. As a result this function should not be used with
        /// large or infinite sequences. The function makes no assumption on the ordering of the original
        /// sequence.
        ///
        /// This is a stable sort, that is the original order of equal elements is preserved.</remarks>
        ///
        /// <param name="projection">A function to transform items of the input sequence into comparable keys.</param>
        ///
        /// <returns>The result sequence.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        member inline source.sortByDescending(projection:('T -> 'Key)) = Seq.sortByDescending projection source

        /// <summary>Returns a sequence that skips 1 element of the underlying sequence and then yields the
        /// remaining elements of the sequence.</summary>
        ///
        ///
        /// <returns>The result sequence.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        /// <exception cref="System.InvalidOperationException">Thrown when the input sequence is empty.</exception>
        member inline source.tail() = Seq.tail source

        /// <summary>Returns the first N elements of the sequence.</summary>
        /// <remarks>Throws <c>InvalidOperationException</c>
        /// if the count exceeds the number of elements in the sequence. <c>Seq.truncate</c>
        /// returns as many items as the sequence contains instead of throwing an exception.</remarks>
        ///
        /// <param name="count">The number of items to take.</param>
        ///
        /// <returns>The result sequence.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown when the input sequence is empty.</exception>
        /// <exception cref="System.InvalidOperationException">Thrown when count exceeds the number of elements
        /// in the sequence.</exception>
        member inline source.take(count:int) = Seq.take count source

        /// <summary>Returns a sequence that, when iterated, yields elements of the underlying sequence while the
        /// given predicate returns <c>true</c>, and then returns no further elements.</summary>
        ///
        /// <param name="predicate">A function that evaluates to false when no more items should be returned.</param>
        ///
        /// <returns>The result sequence.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        member inline source.takeWhile(predicate:('T -> bool)) = Seq.takeWhile predicate source

        /// <summary>Builds an array from the given collection.</summary>
        ///
        ///
        /// <returns>The result array.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        member inline source.toArray() = Seq.toArray source

        /// <summary>Builds a list from the given collection.</summary>
        ///
        ///
        /// <returns>The result list.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        member inline source.toList() = Seq.toList source

        /// <summary>Returns the first element for which the given function returns <c>true</c>.
        /// Return <c>None</c> if no such element exists.</summary>
        ///
        /// <param name="predicate">A function that evaluates to a Boolean when given an item in the sequence.</param>
        ///
        /// <returns>The found element or <c>None</c>.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        member inline source.tryFind(predicate:('T -> bool)) = Seq.tryFind predicate source

        /// <summary>Returns the last element for which the given function returns <c>true</c>.
        /// Return <c>None</c> if no such element exists.</summary>
        ///
        /// <remarks>This function digests the whole initial sequence as soon as it is called. As a
        /// result this function should not be used with large or infinite sequences.</remarks>
        ///
        /// <param name="predicate">A function that evaluates to a Boolean when given an item in the sequence.</param>
        ///
        /// <returns>The found element or <c>None</c>.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        member inline source.tryFindBack(predicate:('T -> bool)) = Seq.tryFindBack predicate source

        /// <summary>Returns the index of the first element in the sequence
        /// that satisfies the given predicate. Return <c>None</c> if no such element exists.</summary>
        ///
        /// <param name="predicate">A function that evaluates to a Boolean when given an item in the sequence.</param>
        ///
        /// <returns>The found index or <c>None</c>.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        member inline source.tryFindIndex(predicate:('T -> bool)) = Seq.tryFindIndex predicate source

        /// <summary>Tries to find the nth element in the sequence.
        /// Returns <c>None</c> if index is negative or the input sequence does not contain enough elements.</summary>
        ///
        /// <param name="index">The index of element to retrieve.</param>
        ///
        /// <returns>The nth element of the sequence or <c>None</c>.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        member inline source.tryItem(index:int) = Seq.tryItem index source

        /// <summary>Returns the index of the last element in the sequence
        /// that satisfies the given predicate. Return <c>None</c> if no such element exists.</summary>
        ///
        /// <remarks>This function digests the whole initial sequence as soon as it is called. As a
        /// result this function should not be used with large or infinite sequences.</remarks>
        /// <param name="predicate">A function that evaluates to a Boolean when given an item in the sequence.</param>
        ///
        /// <returns>The found index or <c>None</c>.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        member inline source.tryFindIndexBack(predicate:('T -> bool)) = Seq.tryFindIndexBack predicate source

        /// <summary>Applies the given function to successive elements, returning the first
        /// result where the function returns "Some(x)".</summary>
        ///
        /// <param name="chooser">A function that transforms items from the input sequence into options.</param>
        ///
        /// <returns>The chosen element or <c>None</c>.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        member inline source.tryPick(chooser:('T -> 'U option)) = Seq.tryPick chooser source

        /// <summary>Returns a sequence that when enumerated returns at most N elements.</summary>
        ///
        /// <param name="count">The maximum number of items to enumerate.</param>
        ///
        /// <returns>The result sequence.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        member inline source.truncate(count:int) = Seq.truncate count source

        /// <summary>Returns a sequence that yields sliding windows containing elements drawn from the input
        /// sequence. Each window is returned as a fresh array.</summary>
        ///
        /// <param name="windowSize">The number of elements in each window.</param>
        ///
        /// <returns>The result sequence.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown when windowSize is not positive.</exception>
        member inline source.windowed(windowSize:int) = Seq.windowed windowSize source

        /// <summary>Combines the two sequences into a list of pairs. The two sequences need not have equal lengths:
        /// when one sequence is exhausted any remaining elements in the other
        /// sequence are ignored.</summary>
        ///
        /// <param name="source2">The second input sequence.</param>
        ///
        /// <returns>The result sequence.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when either of the input sequences is null.</exception>
        member inline source.zip(source2:seq<'T2>) = Seq.zip source source2

        /// <summary>Combines the three sequences into a list of triples. The sequences need not have equal lengths:
        /// when one sequence is exhausted any remaining elements in the other
        /// sequences are ignored.</summary>
        ///
        /// <param name="source2">The second input sequence.</param>
        /// <param name="source3">The third input sequence.</param>
        ///
        /// <returns>The result sequence.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when any of the input sequences is null.</exception>
        member inline source.zip3(source2:seq<'T2>, source3:seq<'T3>) = Seq.zip3 source source2 source3

        /// <summary>Returns the transpose of the given sequence of sequences.</summary>
        ///
        /// <remarks>This function returns a sequence that digests the whole initial sequence as soon as
        /// that sequence is iterated. As a result this function should not be used with
        /// large or infinite sequences.</remarks>
        ///
        ///
        /// <returns>The transposed sequence.</returns>
        ///
        /// <exception cref="T:System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        [<Extension>]
        static member inline transpose(source: seq<#seq<'T>>) = Seq.transpose source

    [<Extension>]
    /// <summary>Fluent extension operations on sequences which require constrained types.</summary>
    type SeqExtensionsConstrained =

        /// <summary>Returns a new sequence that contains all pairings of elements from the first and second sequences.</summary>
        ///
        /// <param name="source2">The second input sequence.</param>
        /// <exception cref="System.ArgumentException">Thrown when either of the input sequences is null.</exception>
        ///
        /// <returns>The resulting sequence of pairs.</returns>
        [<Extension>]
        static member inline allPairs(source: seq<'T>, source2: seq<'U>) = Seq.allPairs source source2

        /// <summary>Return a new sequence with a new item inserted before the given index.</summary>
        ///
        /// <param name="index">The index where the item should be inserted.</param>
        /// <param name="value">The value to insert.</param>
        /// <exception cref="System.ArgumentException">Thrown when index is below 0 or greater than source.Length.</exception>
        ///
        /// <returns>The result sequence.</returns>
        [<Extension>]
        static member inline insertAt(source: seq<'T>, index, value) = Seq.insertAt index value source

        /// <summary>Return a new sequence with new items inserted before the given index.</summary>
        ///
        /// <param name="index">The index where the items should be inserted.</param>
        /// <param name="values">The values to insert.</param>
        /// <exception cref="System.ArgumentException">Thrown when index is below 0 or greater than source.Length.</exception>
        ///
        /// <returns>The result sequence.</returns>
        [<Extension>]
        static member inline insertManyAt(source: seq<'T>, index, values) = Seq.insertManyAt index values source

        /// <summary>Return a new sequence with the item at a given index removed.</summary>
        ///
        /// <param name="index">The index of the item to be removed.</param>
        /// <exception cref="System.ArgumentException">Thrown when index is outside 0..source.Length - 1</exception>
        ///
        /// <returns>The result sequence.</returns>
        [<Extension>]
        static member inline removeAt(source: seq<'T>, index) = Seq.removeAt index source

        /// <summary>Return a new sequence with the number of items starting at a given index removed.</summary>
        ///
        /// <param name="index">The index of the item to be removed.</param>
        /// <param name="count">The number of items to remove.</param>
        /// <exception cref="System.ArgumentException">Thrown when index is outside 0..source.Length - count</exception>
        ///
        /// <returns>The result sequence.</returns>
        [<Extension>]
        static member inline removeManyAt(source: seq<'T>, index, count) = Seq.removeManyAt index count source

        /// <summary>Returns the only element of the sequence or <c>None</c> if it is empty or contains more than one element.</summary>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        ///
        /// <returns>The only element of the sequence or None.</returns>
        [<Extension>]
        static member inline tryExactlyOne(source: seq<'T>) = Seq.tryExactlyOne source

        /// <summary>Return a new sequence with the item at a given index set to the new value.</summary>
        ///
        /// <param name="index">The index of the item to be replaced.</param>
        /// <param name="value">The new value.</param>
        ///
        /// <exception cref="System.ArgumentException">Thrown when index is outside 0..source.Length - 1</exception>
        ///
        /// <returns>The result sequence.</returns>
        [<Extension>]
        static member inline updateAt(source: seq<'T>, index, value) = Seq.updateAt index value source

        /// <summary>Returns the average of the elements in the sequence.</summary>
        ///
        /// <remarks>The elements are averaged using the <c>+</c> operator, <c>DivideByInt</c> method and <c>Zero</c> property
        /// associated with the element type.</remarks>
        ///
        /// <param name="source">The input sequence.</param>
        ///
        /// <returns>The average.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown when the input sequence has zero elements.</exception>
        [<Extension>]
        static member inline average(source: seq<'T>) = Seq.average source

        /// <summary>Returns the average of the results generated by applying the function to each element
        /// of the sequence.</summary>
        ///
        /// <remarks>The elements are averaged using the <c>+</c> operator, <c>DivideByInt</c> method and <c>Zero</c> property
        /// associated with the generated type.</remarks>
        ///
        /// <param name="projection">A function applied to transform each element of the sequence.</param>
        /// <param name="source">The input sequence.</param>
        ///
        /// <returns>The average.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown when the input sequence has zero elements.</exception>
        [<Extension>]
        static member inline averageBy(source: seq<'T>, projection:('T -> 'U)) = Seq.averageBy projection source

        /// <summary>Tests if the sequence contains the specified element.</summary>
        ///
        /// <param name="value">The value to locate in the input sequence.</param>
        /// <param name="source">The input sequence.</param>
        ///
        /// <returns>True if the input sequence contains the specified element; false otherwise.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        [<Extension>]
        static member inline contains(source: seq<'T>, value:'T) = Seq.contains value source

        /// <summary>Returns a sequence that contains no duplicate entries according to generic hash and
        /// equality comparisons on the entries.
        /// If an element occurs multiple times in the sequence then the later occurrences are discarded.</summary>
        ///
        /// <param name="source">The input sequence.</param>
        ///
        /// <returns>The result sequence.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        [<Extension>]
        static member inline distinct(source:seq<'T>) = Seq.distinct source

        /// <summary>Returns a new sequence with the distinct elements of the second sequence which do not apear in the first sequence,
        /// using generic hash and equality comparisons to compare values.</summary>
        ///
        /// <remarks>Note that this function returns a sequence that digests the whole of the first input sequence as soon as
        /// the result sequence is iterated. As a result this function should not be used with
        /// large or infinite sequences in the first parameter. The function makes no assumption on the ordering of the first input
        /// sequence.</remarks>
        ///
        /// <param name="itemsToExclude">A sequence whose elements that also occur in the second sequence will cause those elements to be
        /// removed from the returned sequence.</param>
        /// <param name="source">A sequence whose elements that are not also in first will be returned.</param>
        ///
        /// <returns>A sequence that contains the set difference of the elements of two sequences.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when either of the two input sequences is null.</exception>
        [<Extension>]
        static member inline except(source: seq<'T>, itemsToExclude:seq<'T>) = Seq.except itemsToExclude source

        /// <summary>Returns the greatest of all elements of the sequence, compared via Operators.max</summary>
        ///
        /// <param name="source">The input sequence.</param>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown when the input sequence is empty.</exception>
        ///
        /// <returns>The largest element of the sequence.</returns>
        [<Extension>]
        static member inline max    (source: seq<'T>) = Seq.max source

        /// <summary>Returns the greatest of all elements of the sequence, compared via Operators.max on the function result.</summary>
        ///
        /// <param name="projection">A function to transform items from the input sequence into comparable keys.</param>
        /// <param name="source">The input sequence.</param>
        ///
        /// <returns>The largest element of the sequence.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown when the input sequence is empty.</exception>
        [<Extension>]
        static member inline maxBy (source: seq<'T>, projection:('T -> 'U)) = Seq.maxBy projection source

        /// <summary>Returns the lowest of all elements of the sequence, compared via <c>Operators.min</c>.</summary>
        ///
        /// <param name="source">The input sequence.</param>
        ///
        /// <returns>The smallest element of the sequence.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown when the input sequence is empty.</exception>
        [<Extension>]
        static member inline min    (source: seq<'T>) = Seq.min source

        /// <summary>Returns the lowest of all elements of the sequence, compared via Operators.min on the function result.</summary>
        ///
        /// <param name="projection">A function to transform items from the input sequence into comparable keys.</param>
        /// <param name="source">The input sequence.</param>
        ///
        /// <returns>The smallest element of the sequence.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown when the input sequence is empty.</exception>
        [<Extension>]
        static member inline minBy (source: seq<'T>, projection:('T -> 'U)) = Seq.minBy projection source


        /// <summary>Yields a sequence ordered by keys.</summary>
        ///
        /// <remarks>This function returns a sequence that digests the whole initial sequence as soon as
        /// that sequence is iterated. As a result this function should not be used with
        /// large or infinite sequences. The function makes no assumption on the ordering of the original
        /// sequence.
        ///
        /// This is a stable sort, that is the original order of equal elements is preserved.</remarks>
        ///
        /// <param name="source">The input sequence.</param>
        ///
        /// <returns>The result sequence.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        [<Extension>]
        static member inline sort(source: seq<'T>) = Seq.sort source

        /// <summary>Yields a sequence ordered descending by keys.</summary>
        ///
        /// <remarks>This function returns a sequence that digests the whole initial sequence as soon as
        /// that sequence is iterated. As a result this function should not be used with
        /// large or infinite sequences. The function makes no assumption on the ordering of the original
        /// sequence.
        ///
        /// This is a stable sort, that is the original order of equal elements is preserved.</remarks>
        ///
        /// <param name="source">The input sequence.</param>
        ///
        /// <returns>The result sequence.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        [<Extension>]
        static member inline sortDescending(source: seq<'T>) = Seq.sortDescending  source

        /// <summary>Returns the sum of the elements in the sequence.</summary>
        ///
        /// <remarks>The elements are summed using the <c>+</c> operator and <c>Zero</c> property associated with the generated type.</remarks>
        ///
        /// <param name="source">The input sequence.</param>
        ///
        /// <returns>The computed sum.</returns>
        [<Extension>]
        static member inline sum  (source: seq<'T>) = Seq.sum source

        /// <summary>Returns the sum of the results generated by applying the function to each element of the sequence.</summary>
        /// <remarks>The generated elements are summed using the <c>+</c> operator and <c>Zero</c> property associated with the generated type.</remarks>
        ///
        /// <param name="projection">A function to transform items from the input sequence into the type that will be summed.</param>
        /// <param name="source">The input sequence.</param>
        ///
        /// <returns>The computed sum.</returns>
        [<Extension>]
        static member inline sumBy  (source: seq<'T>, projection:('T -> 'U)) = Seq.sumBy projection  source


/// <summary>Fluent extension operations on arrays.</summary>
[<AutoOpen>]
module ArrayExtensions =


    type ``[]``<'T> with
        /// <summary>Returns a new array that contains all pairings of elements from the first and second arrays.</summary>
        ///
        /// <param name="array2">The second input array.</param>
        /// <exception cref="System.ArgumentException">Thrown when either of the input arrays is null.</exception>
        ///
        /// <returns>The resulting array of pairs.</returns>
        member inline array.allPairs array2 = Array.allPairs array array2

        /// <summary>Return a new array with a new item inserted before the given index.</summary>
        ///
        /// <param name="index">The index where the item should be inserted.</param>
        /// <param name="value">The value to insert.</param>
        /// <exception cref="System.ArgumentException">Thrown when index is below 0 or greater than source.Length.</exception>
        ///
        /// <returns>The result array.</returns>
        member inline array.insertAt(index, value) = Array.insertAt index value array

        /// <summary>Return a new array with new items inserted before the given index.</summary>
        ///
        /// <param name="index">The index where the items should be inserted.</param>
        /// <param name="values">The values to insert.</param>
        /// <exception cref="System.ArgumentException">Thrown when index is below 0 or greater than source.Length.</exception>
        ///
        /// <returns>The result array.</returns>
        member inline array.insertManyAt(index, values) = Array.insertManyAt index values array

        /// <summary>Return a new array with the item at a given index removed.</summary>
        ///
        /// <param name="index">The index of the item to be removed.</param>
        /// <exception cref="System.ArgumentException">Thrown when index is outside 0..source.Length - 1</exception>
        ///
        /// <returns>The result array.</returns>
        member inline array.removeAt index = Array.removeAt index array

        /// <summary>Return a new array with the number of items starting at a given index removed.</summary>
        ///
        /// <param name="index">The index of the item to be removed.</param>
        /// <param name="count">The number of items to remove.</param>
        /// <exception cref="System.ArgumentException">Thrown when index is outside 0..source.Length - count</exception>
        ///
        /// <returns>The result array.</returns>
        member inline array.removeManyAt(index, count) = Array.removeManyAt index count array

        /// <summary>Splits an array into two arrays, at the given index.</summary>
        ///
        /// <param name="index">The index at which the array is split.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when the input array is null.</exception>
        /// <exception cref="System.InvalidOperationException">Thrown when split index exceeds the number of elements in the array.</exception>
        ///
        /// <returns>The two split arrays.</returns>
        member inline array.splitAt index = Array.splitAt index array

        /// <summary>Returns the only element of the array or <c>None</c> if it is empty or contains more than one element.</summary>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input array is null.</exception>
        ///
        /// <returns>The only element of the array or None.</returns>
        member inline array.tryExactlyOne() = Array.tryExactlyOne array

        /// <summary>Return a new array with the item at a given index set to the new value.</summary>
        ///
        /// <param name="index">The index of the item to be replaced.</param>
        /// <param name="value">The new value.</param>
        ///
        /// <exception cref="System.ArgumentException">Thrown when index is outside 0..source.Length - 1</exception>
        ///
        /// <returns>The result array.</returns>
        member inline array.updateAt(index, value) = Array.updateAt index value array

        /// <summary>Builds a new array that contains the elements of the first array followed by the elements of the second array.</summary>
        ///
        /// <param name="array2">The second input array.</param>
        ///
        /// <returns>The resulting array.</returns>
        member inline array.append array2 = Array.append array array2

        /// <summary>For each element of the array, applies the given function. Concatenates all the results and return the combined array.</summary>
        ///
        /// <param name="mapping">The function to create sub-arrays from the input array elements.</param>
        ///
        /// <returns>The concatenation of the sub-arrays.</returns>
        member inline array.collect mapping = Array.collect mapping array

        /// <summary>Builds a new array that contains the elements of the given array.</summary>
        ///
        /// <returns>A copy of the input array.</returns>
        member inline array.copy() = Array.copy array

        /// <summary>Applies the given function to successive elements, returning the first
        /// result where function returns <c>Some(x)</c> for some <c>x</c>. If the function
        /// never returns <c>Some(x)</c> then <c>None</c> is returned.</summary>
        /// <param name="chooser">The function to transform the array elements into options.</param>
        /// <returns>The first transformed element that is <c>Some(x)</c>.</returns>
        member inline array.tryPick chooser = Array.tryPick chooser array

        /// <summary>Applies the given function to successive elements, returning the first
        /// result where function returns <c>Some(x)</c> for some <c>x</c>. If the function
        /// never returns <c>Some(x)</c> then <c>KeyNotFoundException</c> is raised.</summary>
        ///
        /// <param name="chooser">The function to generate options from the elements.</param>
        ///
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">Thrown if every result from
        /// <c>chooser</c> is <c>None</c>.</exception>
        ///
        /// <returns>The first result.</returns>
        member inline array.pick chooser = Array.pick chooser array

        /// <summary>Applies the given function to each element of the array. Returns
        /// the array comprised of the results "x" for each element where
        /// the function returns Some(x)</summary>
        /// <param name="chooser">The function to generate options from the elements.</param>
        /// <returns>The array of results.</returns>
        member inline array.choose chooser = Array.choose chooser array

        /// <summary>Applies a key-generating function to each element of an array and returns an array yielding unique
        /// keys and their number of occurrences in the original array.</summary>
        ///
        /// <param name="projection">A function transforming each item of the input array into a key to be
        /// compared against the others.</param>
        ///
        /// <returns>The result array.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input array is null.</exception>
        member inline array.countBy(projection:('T -> 'Key)) = Array.countBy projection array

        /// <summary>Returns an array that contains no duplicate entries according to the
        /// generic hash and equality comparisons on the keys returned by the given key-generating function.
        /// If an element occurs multiple times in the array then the later occurrences are discarded.</summary>
        ///
        /// <param name="projection">A function transforming the array items into comparable keys.</param>
        ///
        /// <returns>The result array.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input array is null.</exception>
        member inline array.distinctBy(projection:('T -> 'Key)) = Array.distinctBy projection array

        /// <summary>Applies a key-generating function to each element of an array and yields an array of
        /// unique keys. Each unique key contains an array of all elements that match
        /// to this key.</summary>
        ///
        /// <param name="projection">A function that transforms an element of the array into a comparable key.</param>
        ///
        /// <returns>The result array.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input array is null.</exception>
        member inline array.groupBy(projection:('T -> 'Key)) = Array.groupBy projection array

        /// <summary>Returns an array of each element in the input array and its predecessor, with the
        /// exception of the first element which is only returned as the predecessor of the second element.</summary>
        ///
        /// <returns>The result array.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        member inline array.pairwise() = Array.pairwise array

        /// <summary>Builds a new array that contains the elements of the given array, excluding the first N elements.</summary>
        ///
        /// <param name="count">The number of elements to skip.</param>
        ///
        /// <returns>A copy of the input array, after removing the first N elements.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input array is null.</exception>
        /// <exception cref="System.ArgumentExcepion">Thrown when count is negative or exceeds the number of
        /// elements in the array.</exception>
        member inline array.skip(count:int) = Array.skip count array

        /// <summary>Bypasses elements in an array while the given predicate returns <c>true</c>, and then returns
        /// the remaining elements in a new array.</summary>
        ///
        /// <param name="predicate">A function that evaluates an element of the array to a boolean value.</param>
        ///
        /// <returns>The created sub array.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input array is null.</exception>
        member inline array.skipWhile(predicate:('T -> bool)) = Array.skipWhile predicate array

        /// <summary>Returns the first N elements of the array.</summary>
        /// <remarks>Throws <c>InvalidOperationException</c>
        /// if the count exceeds the number of elements in the array. <c>Array.truncate</c>
        /// returns as many items as the array contains instead of throwing an exception.</remarks>
        ///
        /// <param name="count">The number of items to take.</param>
        ///
        /// <returns>The result array.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input array is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown when the input array is empty.</exception>
        /// <exception cref="System.InvalidOperationException">Thrown when count exceeds the number of elements
        /// in the array.</exception>
        member inline array.take(count:int) = Array.take count array

        /// <summary>Returns an array that contains all elements of the original array while the
        /// given predicate returns <c>true</c>, and then returns no further elements.</summary>
        ///
        /// <param name="predicate">A function that evaluates to false when no more items should be returned.</param>
        ///
        /// <returns>The result array.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input array is null.</exception>
        member inline array.takeWhile(predicate:('T -> bool)) = Array.takeWhile predicate array

        /// <summary>Returns at most N elements in a new array.</summary>
        ///
        /// <param name="count">The maximum number of items to return.</param>
        ///
        /// <returns>The result array.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input array is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown when the count is negative.</exception>
        member inline array.truncate(count:int) = Array.truncate count array

        /// <summary>Returns a new array containing only the elements of the array
        /// for which the given predicate returns "true".</summary>
        /// <param name="predicate">The function to test the input elements.</param>
        /// <returns>An array containing the elements for which the given predicate returns true.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input array is null.</exception>
        member inline array.where(predicate:('T -> bool)) = Array.where predicate array

        /// <summary>Returns an array of sliding windows containing elements drawn from the input
        /// array. Each window is returned as a fresh array.</summary>
        ///
        /// <param name="windowSize">The number of elements in each window.</param>
        ///
        /// <returns>The result array.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input array is null.</exception>
        ///
        /// <exception cref="System.ArgumentException">Thrown when windowSize is not positive.</exception>
        member inline array.windowed(windowSize:int) = Array.windowed windowSize array

        /// <summary>Builds a new array whose elements are the corresponding elements of the input array
        /// paired with the integer index (from 0) of each element.</summary>
        ///
        /// <returns>The array of indexed elements.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input array is null.</exception>
        member inline array.indexed() = Array.indexed array

        /// <summary>Tests if any element of the array satisfies the given predicate.</summary>
        ///
        /// <remarks>The predicate is applied to the elements of the input array. If any application
        /// returns true then the overall result is true and no further elements are tested.
        /// Otherwise, false is returned.</remarks>
        ///
        /// <param name="predicate">The function to test the input elements.</param>
        ///
        /// <returns>True if any result from <c>predicate</c> is true.</returns>
        member inline array.exists predicate = Array.exists predicate array

        /// <summary>Returns a new collection containing only the elements of the collection
        /// for which the given predicate returns "true".</summary>
        ///
        /// <param name="predicate">The function to test the input elements.</param>
        ///
        /// <returns>An array containing the elements for which the given predicate returns true.</returns>
        member inline array.filter predicate = Array.filter predicate array

        /// <summary>Returns the first element for which the given function returns 'true'.
        /// Raise <c>KeyNotFoundException</c> if no such element exists.</summary>
        ///
        /// <param name="predicate">The function to test the input elements.</param>
        ///
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">Thrown if <c>predicate</c>
        /// never returns true.</exception>
        ///
        /// <returns>The first element for which <c>predicate</c> returns true.</returns>
        member inline array.find predicate = Array.find predicate array

        /// <summary>Returns the index of the first element in the array
        /// that satisfies the given predicate. Raise <c>KeyNotFoundException</c> if
        /// none of the elements satisy the predicate.</summary>
        ///
        /// <param name="predicate">The function to test the input elements.</param>
        ///
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">Thrown if <c>predicate</c>
        /// never returns true.</exception>
        ///
        /// <returns>The index of the first element in the array that satisfies the given predicate.</returns>
        member inline array.findIndex predicate = Array.findIndex predicate array

        /// <summary>Tests if all elements of the array satisfy the given predicate.</summary>
        ///
        /// <remarks>The predicate is applied to the elements of the input collection. If any application
        /// returns false then the overall result is false and no further elements are tested.
        /// Otherwise, true is returned.</remarks>
        ///
        /// <param name="predicate">The function to test the input elements.</param>
        ///
        /// <returns>True if all of the array elements satisfy the predicate.</returns>
        member inline array.forall predicate = Array.forall predicate array

        /// <summary>Applies a function to each element of the collection, threading an accumulator argument
        /// through the computation. If the input function is <c>f</c> and the elements are <c>i0...iN</c> then computes
        /// <c>f (... (f s i0)...) iN</c></summary>
        ///
        /// <param name="folder">The function to update the state given the input elements.</param>
        /// <param name="state">The initial state.</param>
        ///
        /// <returns>The final state.</returns>
        member inline array.fold(state:'State, folder:('State -> 'T -> 'State)) = Array.fold folder state array

        /// <summary>Applies a function to each element of the array, threading an accumulator argument
        /// through the computation. If the input function is <c>f</c> and the elements are <c>i0...iN</c> then computes
        /// <c>f i0 (...(f iN s))</c></summary>
        ///
        /// <param name="folder">The function to update the state given the input elements.</param>
        /// <param name="state">The initial state.</param>
        ///
        /// <returns>The final state.</returns>
        member inline array.foldBack(folder:('T -> 'State -> 'State), state: 'State) = Array.foldBack folder array state

        /// <summary>Returns true if the given array is empty, otherwise false.</summary>
        ///
        /// <returns>True if the array is empty.</returns>
        member inline array.IsEmpty = Array.isEmpty array

        /// <summary>Applies the given function to each element of the array.</summary>
        ///
        /// <param name="action">The function to apply.</param>
        member inline array.iter action = Array.iter action array

        /// <summary>Applies the given function to each element of the array. The integer passed to the
        /// function indicates the index of element.</summary>
        ///
        /// <param name="action">The function to apply to each index and element.</param>
        member inline array.iteri action = Array.iteri action array

        /// <summary>Returns the length of an array. You can also use property array.Length.</summary>
        ///
        /// <returns>The length of the array.</returns>
        member inline array.length = Array.length array

        /// <summary>Builds a new array whose elements are the results of applying the given function
        /// to each of the elements of the array.</summary>
        ///
        /// <param name="mapping">The function to transform elements of the array.</param>
        ///
        /// <returns>The array of transformed elements.</returns>
        member inline array.map mapping = Array.map mapping array

        /// <summary>Builds a new array whose elements are the results of applying the given function
        /// to each of the elements of the array. The integer index passed to the
        /// function indicates the index of element being transformed.</summary>
        ///
        /// <param name="mapping">The function to transform elements and their indices.</param>
        ///
        /// <returns>The array of transformed elements.</returns>
        member inline array.mapi mapping = Array.mapi mapping array

        /// <summary>Splits the collection into two collections, containing the
        /// elements for which the given predicate returns "true" and "false"
        /// respectively.</summary>
        ///
        /// <param name="predicate">The function to test the input elements.</param>
        ///
        /// <returns>A pair of arrays. The first containing the elements the predicate evaluated to true,
        /// and the second containing those evaluated to false.</returns>
        member inline array.partition predicate = Array.partition predicate array

        /// <summary>Returns an array with all elements permuted according to the
        /// specified permutation.</summary>
        ///
        /// <param name="indexMap">The function that maps input indices to output indices.</param>
        ///
        /// <returns>The output array.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input array is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown when indexMap does not produce a valid permutation.</exception>
        member inline array.permute indexMap = Array.permute indexMap array

        /// <summary>Applies a function to each element of the array, threading an accumulator argument
        /// through the computation. If the input function is <c>f</c> and the elements are <c>i0...iN</c>
        /// then computes <c>f (... (f i0 i1)...) iN</c>.
        /// Raises ArgumentException if the array has size zero.</summary>
        ///
        /// <param name="reduction">The function to reduce a pair of elements to a single element.</param>
        ///
        /// <exception cref="System.ArgumentException">Thrown when the input array is empty.</exception>
        ///
        /// <returns>The final result of the redcutions.</returns>
        member inline array.reduce reduction = Array.reduce reduction array

        /// <summary>Applies a function to each element of the array, threading an accumulator argument
        /// through the computation. If the input function is <c>f</c> and the elements are <c>i0...iN</c>
        /// then computes <c>f i0 (...(f iN-1 iN))</c>.
        /// Raises ArgumentException if the array has size zero.</summary>
        ///
        /// <param name="reduction">The function to reduce a pair of elements to a single element.</param>
        ///
        /// <exception cref="System.ArgumentException">Thrown when the input array is empty.</exception>
        ///
        /// <returns>The final result of the reductions.</returns>
        member inline array.reduceBack reduction = Array.reduceBack reduction array

        /// <summary>Returns a new array with the elements in reverse order.</summary>
        /// <returns>The reversed array.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when the input array is null.</exception>
        member inline array.reverse() = Array.rev array

        /// <summary>Like <c>fold</c>, but return the intermediary and final results.</summary>
        ///
        /// <param name="folder">The function to update the state given the input elements.</param>
        /// <param name="state">The initial state.</param>
        ///
        /// <returns>The array of state values.</returns>
        member inline array.scan(state:'State, folder:('State -> 'T -> 'State)) = Array.scan folder state array

        /// <summary>Like <c>foldBack</c>, but return both the intermediary and final results.</summary>
        ///
        /// <param name="folder">The function to update the state given the input elements.</param>
        /// <param name="state">The initial state.</param>
        ///
        /// <returns>The array of state values.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input array is null.</exception>
        member inline array.scanBack(folder:('T -> 'State -> 'State), state:'State) = Array.scanBack folder array state

        /// <summary>Sorts the elements of an array, using the given projection for the keys and returning a new array.
        /// Elements are compared using Operators.compare.</summary>
        ///
        /// <remarks>This is not a stable sort, i.e. the original order of equal elements is not necessarily preserved.
        /// For a stable sort, consider using Seq.sort.</remarks>
        ///
        /// <param name="projection">The function to transform array elements into the type that is compared.</param>
        ///
        /// <returns>The sorted array.</returns>
        member inline array.sortBy(projection:('T -> 'Key)) = Array.sortBy projection array

        /// <summary>Sorts the elements of an array, using the given comparison function as the order, returning a new array.</summary>
        ///
        /// <remarks>This is not a stable sort, i.e. the original order of equal elements is not necessarily preserved.
        /// For a stable sort, consider using Seq.sort.</remarks>
        ///
        /// <param name="comparer">The function to compare pairs of array elements.</param>
        ///
        /// <returns>The sorted array.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input array is null.</exception>
        member inline array.sortWith(comparer) = Array.sortWith comparer array

        /// <summary>Sorts the elements of an array by mutating the array in-place, using the given projection for the keys.
        /// Elements are compared using Operators.compare.</summary>
        ///
        /// <remarks>This is not a stable sort, i.e. the original order of equal elements is not necessarily preserved.
        /// For a stable sort, consider using Seq.sort.</remarks>
        ///
        /// <param name="projection">The function to transform array elements into the type that is compared.</param>
        member inline array.sortInPlaceBy(projection:('T -> 'Key)) = Array.sortInPlaceBy projection array

        /// <summary>Sorts the elements of an array by mutating the array in-place, using the given comparison function as the order.</summary>
        /// <param name="comparer">The function to compare pairs of array elements.</param>
        member inline array.sortInPlaceWith(comparer) = Array.sortInPlaceWith comparer array

        /// <summary>Builds a list from the given array.</summary>
        /// <returns>The list of array elements.</returns>
        member inline array.toList() = Array.toList array

        /// <summary>Views the given array as a sequence.</summary>
        /// <returns>The sequence of array elements.</returns>
        member inline array.toSeq() = Array.toSeq array

        /// <summary>Returns the first element for which the given function returns <c>true</c>.
        /// Return <c>None</c> if no such element exists.</summary>
        /// <param name="predicate">The function to test the input elements.</param>
        /// <returns>The first element that satisfies the predicate, or None.</returns>
        member inline array.tryFind predicate = Array.tryFind predicate array

        /// <summary>Returns the index of the first element in the array
        /// that satisfies the given predicate.</summary>
        /// <param name="predicate">The function to test the input elements.</param>
        /// <returns>The index of the first element that satisfies the predicate, or None.</returns>
        member inline array.tryFindIndex predicate = Array.tryFindIndex predicate array

        /// <summary>Combines the two arrays into an array of pairs. The two arrays must have equal lengths, otherwise an <c>ArgumentException</c> is
        /// raised.</summary>
        /// <param name="array2">The second input array.</param>
        /// <exception cref="System.ArgumentException">Thrown when the input arrays differ in length.</exception>
        /// <returns>The array of tupled elements.</returns>
        member inline array.zip array2 = Array.zip array array2

        /// <summary>Combines three arrays into an list of pairs. The three arrays must have equal lengths, otherwise an <c>ArgumentException</c> is
        /// raised.</summary>
        ///
        /// <param name="array2">The second input list.</param>
        /// <param name="array3">The third input list.</param>
        ///
        /// <exception cref="System.ArgumentException">Thrown when the input arrays differ in length.</exception>
        ///
        /// <returns>The list of tupled elements.</returns>
        member inline array.zip3(array2, array3) = Array.zip3 array array2 array3

        /// <summary>Returns the only element of the array.</summary>
        ///
        /// <returns>The only element of the array.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input array is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown when the input does not have precisely one element.</exception>
        member inline array.exactlyOne() = Array.exactlyOne array

        /// <summary>Sorts the elements of an array, in descending order, using the given projection for the keys and returning a new array.
        /// Elements are compared using Operators.compare.</summary>
        ///
        /// <remarks>This is not a stable sort, i.e. the original order of equal elements is not necessarily preserved.
        /// For a stable sort, consider using Seq.sort.</remarks>
        ///
        /// <param name="projection">The function to transform array elements into the type that is compared.</param>
        ///
        /// <returns>The sorted array.</returns>
        member inline array.sortByDescending(projection:('T -> 'Key)) = Array.sortByDescending projection array

        /// <summary>Returns the index of the last element in the array
        /// that satisfies the given predicate.</summary>
        ///
        /// <param name="predicate">The function to test the input elements.</param>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input array is null.</exception>
        ///
        /// <returns>The index of the last element that satisfies the predicate, or None.</returns>
        member inline array.tryFindIndexBack(predicate:('T -> bool)) = Array.tryFindIndexBack predicate array

        /// <summary>Returns the last element for which the given function returns <c>true</c>.
        /// Return <c>None</c> if no such element exists.</summary>
        ///
        /// <param name="predicate">The function to test the input elements.</param>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input array is null.</exception>
        ///
        /// <returns>The last element that satisfies the predicate, or None.</returns>
        member inline array.tryFindBack(predicate:('T -> bool)) = Array.tryFindBack predicate array

        /// <summary>Tries to find the nth element in the array.
        /// Returns <c>None</c> if index is negative or the input array does not contain enough elements.</summary>
        ///
        /// <param name="index">The index of element to retrieve.</param>
        ///
        /// <returns>The nth element of the array or <c>None</c>.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input array is null.</exception>
        member inline array.tryItem(index:int) = Array.tryItem index array

        /// <summary>Returns a new array containing the elements of the original except the first element.</summary>
        ///
        /// <exception cref="System.ArgumentException">Thrown when the array is empty.</exception>
        /// <exception cref="System.ArgumentNullException">Thrown when the input array is null.</exception>
        ///
        /// <returns>A new array containing the elements of the original except the first element.</returns>
        member inline array.tail() = Array.tail array

        /// <summary>Returns the first element of the array.</summary>
        ///
        /// <returns>The first element of the array.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input array is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown when the input array is empty.</exception>
        member inline array.head() = Array.head array

        /// <summary>Returns the last element of the array.</summary>
        /// <returns>The last element of the array.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when the input array is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown when the input does not have any elements.</exception>
        member inline array.last() = Array.last array

        /// <summary>Returns the last element of the array.
        /// Return <c>None</c> if no such element exists.</summary>
        ///
        /// <returns>The last element of the array or None.</returns>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        member inline array.tryLast() = Array.tryLast array

        /// <summary>Returns the first element of the array, or
        /// <c>None</c> if the array is empty.</summary>
        ///
        /// <exception cref="System.ArgumentNullException">Thrown when the input array is null.</exception>
        ///
        /// <returns>The first element of the array or None.</returns>
        member inline array.tryHead() = Array.tryHead array

        /// <summary>Returns the transpose of the given sequence of arrays.</summary>
        ///
        /// <param name="arrays">The input sequence of arrays.</param>
        ///
        /// <returns>The transposed array.</returns>
        ///
        /// <exception cref="T:System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        /// <exception cref="T:System.ArgumentException">Thrown when the input arrays differ in length.</exception>
        [<Extension>]
        static member inline transpose(arrays: seq<'T[]>) = Array.transpose arrays

[<Extension>]
/// <summary>Fluent extension operations on arrays which require constrained types.</summary>
type ArrayExtensionsConstrained =


    /// <summary>Returns a new array that contains all pairings of elements from the first and second arrays.</summary>
    ///
    /// <param name="array2">The second input array.</param>
    /// <exception cref="System.ArgumentException">Thrown when either of the input arrays is null.</exception>
    ///
    /// <returns>The resulting array of pairs.</returns>
    [<Extension>]
    static member inline allPairs(array:'T[], array2:'U[]) = Array.allPairs array array2

    /// <summary>Return a new array with a new item inserted before the given index.</summary>
    ///
    /// <param name="index">The index where the item should be inserted.</param>
    /// <param name="value">The value to insert.</param>
    /// <exception cref="System.ArgumentException">Thrown when index is below 0 or greater than source.Length.</exception>
    ///
    /// <returns>The result array.</returns>
    [<Extension>]
    static member inline insertAt(array:'T[], index, value) = Array.insertAt index value array

    /// <summary>Return a new array with new items inserted before the given index.</summary>
    ///
    /// <param name="index">The index where the items should be inserted.</param>
    /// <param name="values">The values to insert.</param>
    /// <exception cref="System.ArgumentException">Thrown when index is below 0 or greater than source.Length.</exception>
    ///
    /// <returns>The result array.</returns>
    [<Extension>]
    static member inline insertManyAt(array:'T[], index, values) = Array.insertManyAt index values array

    /// <summary>Return a new array with the item at a given index removed.</summary>
    ///
    /// <param name="index">The index of the item to be removed.</param>
    /// <exception cref="System.ArgumentException">Thrown when index is outside 0..source.Length - 1</exception>
    ///
    /// <returns>The result array.</returns>
    [<Extension>]
    static member inline removeAt(array:'T[], index) = Array.removeAt index array

    /// <summary>Return a new array with the number of items starting at a given index removed.</summary>
    ///
    /// <param name="index">The index of the item to be removed.</param>
    /// <param name="count">The number of items to remove.</param>
    /// <exception cref="System.ArgumentException">Thrown when index is outside 0..source.Length - count</exception>
    ///
    /// <returns>The result array.</returns>
    [<Extension>]
    static member inline removeManyAt(array:'T[], index, count) = Array.removeManyAt index count array

    /// <summary>Splits an array into two arrays, at the given index.</summary>
    ///
    /// <param name="index">The index at which the array is split.</param>
    /// <exception cref="System.ArgumentNullException">Thrown when the input array is null.</exception>
    /// <exception cref="System.InvalidOperationException">Thrown when split index exceeds the number of elements in the array.</exception>
    ///
    /// <returns>The two split arrays.</returns>
    [<Extension>]
    static member inline splitAt(array:'T[], index) = Array.splitAt index array

    /// <summary>Returns the only element of the array or <c>None</c> if it is empty or contains more than one element.</summary>
    ///
    /// <exception cref="System.ArgumentNullException">Thrown when the input array is null.</exception>
    ///
    /// <returns>The only element of the array or None.</returns>
    [<Extension>]
    static member inline tryExactlyOne(array:'T[]) = Array.tryExactlyOne array

    /// <summary>Return a new array with the item at a given index set to the new value.</summary>
    ///
    /// <param name="index">The index of the item to be replaced.</param>
    /// <param name="value">The new value.</param>
    ///
    /// <exception cref="System.ArgumentException">Thrown when index is outside 0..source.Length - 1</exception>
    ///
    /// <returns>The result array.</returns>
    [<Extension>]
    static member inline updateAt(array:'T[], index, value) = Array.updateAt index value array

    /// <summary>Returns the average of the elements in the array.</summary>
    /// <param name="array">The input array.</param>
    /// <exception cref="System.ArgumentException">Thrown when <c>array</c> is empty.</exception>
    /// <returns>The average of the elements in the array.</returns>
    [<Extension>]
    static member inline average(array:'T[]) = Array.average array

    /// <summary>Returns the average of the elements generated by applying the function to each element of the array.</summary>
    ///
    /// <param name="projection">The function to transform the array elements before averaging.</param>
    /// <param name="array">The input array.</param>
    ///
    /// <exception cref="System.ArgumentException">Thrown when <c>array</c> is empty.</exception>
    ///
    /// <returns>The computed average.</returns>
    [<Extension>]
    static member inline averageBy(array:'T[], projection:('T -> 'U)) = Array.averageBy projection  array

    /// <summary>Tests if the array contains the specified element.</summary>
    ///
    /// <param name="value">The value to locate in the input array.</param>
    /// <param name="array">The input array.</param>
    ///
    /// <returns>True if the input array contains the specified element; false otherwise.</returns>
    ///
    /// <exception cref="System.ArgumentNullException">Thrown when the input array is null.</exception>
    [<Extension>]
    static member inline contains(array: 'T[], value:'T) = Array.contains value array

    /// <summary>Returns an array that contains no duplicate entries according to generic hash and
    /// equality comparisons on the entries.
    /// If an element occurs multiple times in the array then the later occurrences are discarded.</summary>
    ///
    /// <param name="array">The input array.</param>
    ///
    /// <returns>The result array.</returns>
    ///
    /// <exception cref="System.ArgumentNullException">Thrown when the input array is null.</exception>
    [<Extension>]
    static member inline distinct(array:'T[]) = Array.distinct array

    /// <summary>Sorts the elements of an array, in descending order, returning a new array. Elements are compared using Operators.compare. </summary>
    ///
    /// <param name="array">The input array.</param>
    ///
    /// <remarks>This is not a stable sort, i.e. the original order of equal elements is not necessarily preserved.
    /// For a stable sort, consider using Seq.sort.</remarks>
    ///
    /// <returns>The sorted array.</returns>
    [<Extension>]
    static member inline sortDescending(array:'T[]) = Array.sortDescending array

    /// <summary>Returns the greatest of all elements of the array, compared via Operators.max on the function result.</summary>
    ///
    /// <remarks>Throws ArgumentException for empty arrays.</remarks>
    ///
    /// <param name="array">The input array.</param>
    ///
    /// <exception cref="System.ArgumentException">Thrown when the input array is empty.</exception>
    ///
    /// <returns>The maximum element.</returns>
    [<Extension>]
    static member inline max(array:'T[]) = Array.max array

    /// <summary>Returns the greatest of all elements of the array, compared via Operators.max on the function result.</summary>
    ///
    /// <remarks>Throws ArgumentException for empty arrays.</remarks>
    ///
    /// <param name="projection">The function to transform the elements into a type supporting comparison.</param>
    /// <param name="array">The input array.</param>
    ///
    /// <exception cref="System.ArgumentException">Thrown when the input array is empty.</exception>
    ///
    /// <returns>The maximum element.</returns>
    [<Extension>]
    static member inline maxBy(array:'T[], projection) = Array.maxBy projection array

    /// <summary>Returns the lowest of all elements of the array, compared via Operators.min.</summary>
    ///
    /// <remarks>Throws ArgumentException for empty arrays</remarks>
    ///
    /// <param name="array">The input array.</param>
    ///
    /// <exception cref="System.ArgumentException">Thrown when the input array is empty.</exception>
    ///
    /// <returns>The minimum element.</returns>
    [<Extension>]
    static member inline min(array:'T[]) = Array.min array

    /// <summary>Returns the lowest of all elements of the array, compared via Operators.min on the function result.</summary>
    ///
    /// <remarks>Throws ArgumentException for empty arrays.</remarks>
    ///
    /// <param name="projection">The function to transform the elements into a type supporting comparison.</param>
    /// <param name="array">The input array.</param>
    ///
    /// <exception cref="System.ArgumentException">Thrown when the input array is empty.</exception>
    ///
    /// <returns>The minimum element.</returns>
    [<Extension>]
    static member inline minBy(array:'T[], projection) = Array.minBy projection array

    /// <summary>Sorts the elements of an array, returning a new array. Elements are compared using Operators.compare. </summary>
    ///
    /// <remarks>This is not a stable sort, i.e. the original order of equal elements is not necessarily preserved.
    /// For a stable sort, consider using Seq.sort.</remarks>
    ///
    /// <param name="array">The input array.</param>
    ///
    /// <returns>The sorted array.</returns>
    [<Extension>]
    static member inline sort(array:'T[]) = Array.sort array

    /// <summary>Sorts the elements of an array by mutating the array in-place, using the given comparison function.
    /// Elements are compared using Operators.compare.</summary>
    ///
    /// <param name="array">The input array.</param>
    [<Extension>]
    static member inline sortInPlace(array) = Array.sortInPlace array

    /// <summary>Returns the sum of the elements in the array.</summary>
    ///
    /// <param name="array">The input array.</param>
    ///
    /// <returns>The resulting sum.</returns>
    [<Extension>]
    static member inline sum(array: 'T[]) = Array.sum array

    /// <summary>Returns the sum of the results generated by applying the function to each element of the array.</summary>
    ///
    /// <param name="projection">The function to transform the array elements into the type to be summed.</param>
    /// <param name="array">The input array.</param>
    ///
    /// <returns>The resulting sum.</returns>
    [<Extension>]
    static member inline sumBy(array: 'T[], projection:('T -> 'U)) = Array.sumBy projection array

    /// <summary>Splits an array of pairs into two arrays.</summary>
    ///
    /// <param name="array">The input array.</param>
    ///
    /// <returns>The two arrays.</returns>
    [<Extension>]
    static member inline unzip(array) = Array.unzip array

    /// <summary>Splits an array of triples into three arrays.</summary>
    ///
    /// <param name="array">The input array.</param>
    ///
    /// <returns>The tuple of three arrays.</returns>
    [<Extension>]
    static member inline unzip3(array) = Array.unzip3 array

/// <summary>Fluent extension operations on lists.</summary>
[<AutoOpen>]
module ListExtensions =

    type List<'T> with
        /// <summary>Returns a new list that contains all pairings of elements from two lists.</summary>
        ///
        /// <param name="list2">The second input list.</param>
        ///
        /// <returns>The resulting list of pairs.</returns>
        member inline list.allPairs list2 = List.allPairs list list2

        /// <summary>Return a new list with a new item inserted before the given index.</summary>
        ///
        /// <param name="index">The index where the item should be inserted.</param>
        /// <param name="value">The value to insert.</param>
        /// <exception cref="System.ArgumentException">Thrown when index is below 0 or greater than source.Length.</exception>
        ///
        /// <returns>The result list.</returns>
        member inline list.insertAt(index, value) = List.insertAt index value list

        /// <summary>Return a new list with new items inserted before the given index.</summary>
        ///
        /// <param name="index">The index where the items should be inserted.</param>
        /// <param name="values">The values to insert.</param>
        /// <exception cref="System.ArgumentException">Thrown when index is below 0 or greater than source.Length.</exception>
        ///
        /// <returns>The result list.</returns>
        member inline list.insertManyAt(index, values) = List.insertManyAt index values list

        /// <summary>Return a new list with the item at a given index removed.</summary>
        ///
        /// <param name="index">The index of the item to be removed.</param>
        /// <exception cref="System.ArgumentException">Thrown when index is outside 0..source.Length - 1</exception>
        ///
        /// <returns>The result list.</returns>
        member inline list.removeAt index = List.removeAt index list

        /// <summary>Return a new list with the number of items starting at a given index removed.</summary>
        ///
        /// <param name="index">The index of the item to be removed.</param>
        /// <param name="count">The number of items to remove.</param>
        /// <exception cref="System.ArgumentException">Thrown when index is outside 0..source.Length - count</exception>
        ///
        /// <returns>The result list.</returns>
        member inline list.removeManyAt(index, count) = List.removeManyAt index count list

        /// <summary>Splits a list into two lists, at the given index.</summary>
        ///
        /// <param name="index">The index at which the list is split.</param>
        /// <exception cref="System.InvalidOperationException">Thrown when split index exceeds the number of elements in the list.</exception>
        ///
        /// <returns>The two split lists.</returns>
        member inline list.splitAt index = List.splitAt index list

        /// <summary>Returns the only element of the list or <c>None</c> if it is empty or contains more than one element.</summary>
        ///
        /// <returns>The only element of the list or None.</returns>
        member inline list.tryExactlyOne() = List.tryExactlyOne list

        /// <summary>Return a new list with the item at a given index set to the new value.</summary>
        ///
        /// <param name="index">The index of the item to be replaced.</param>
        /// <param name="value">The new value.</param>
        ///
        /// <exception cref="System.ArgumentException">Thrown when index is outside 0..source.Length - 1</exception>
        ///
        /// <returns>The result list.</returns>
        member inline list.updateAt(index, value) = List.updateAt index value list

        /// <summary>Builds a new list that contains the elements of the first list followed by the elements of the second list.</summary>
        ///
        /// <param name="list2">The second input list.</param>
        ///
        /// <returns>The resulting list.</returns>
        member inline list.append list2 = List.append list list2

        /// <summary>For each element of the list, applies the given function. Concatenates all the results and return the combined list.</summary>
        ///
        /// <param name="mapping">The function to create sub- lists from the input list elements.</param>
        ///
        /// <returns>The concatenation of the sub- lists.</returns>
        member inline list.collect mapping = List.collect mapping list

        /// <summary>Builds a new list that contains the elements of each of the given sequence of  lists.</summary>
        ///
        /// <param name="lists">The input sequence of  lists.</param>
        ///
        /// <returns>The concatenation of the sequence of input  lists.</returns>
        member inline list.concat( lists) = List.concat (Array.append [| list |]  lists)

        /// <summary>Applies the given function to successive elements, returning the first
        /// result where function returns <c>Some(x)</c> for some <c>x</c>. If the function
        /// never returns <c>Some(x)</c> then <c>None</c> is returned.</summary>
        ///
        /// <param name="chooser">The function to transform the list elements into options.</param>
        ///
        /// <returns>The first transformed element that is <c>Some(x)</c>.</returns>
        member inline list.tryPick chooser = List.tryPick chooser list

        /// <summary>Applies the given function to successive elements, returning the first
        /// result where function returns <c>Some(x)</c> for some <c>x</c>. If the function
        /// never returns <c>Some(x)</c> then <c>KeyNotFoundException</c> is raised.</summary>
        ///
        /// <param name="chooser">The function to generate options from the elements.</param>
        ///
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">Thrown if every result from
        /// <c>chooser</c> is <c>None</c>.</exception>
        ///
        /// <returns>The first result.</returns>
        member inline list.pick chooser = List.pick chooser list

        /// <summary>Applies the given function to each element of the list. Returns
        /// the list comprised of the results "x" for each element where
        /// the function returns Some(x)</summary>
        ///
        /// <param name="chooser">The function to generate options from the elements.</param>
        ///
        /// <returns>The list of results.</returns>
        member inline list.choose chooser = List.choose chooser list

        /// <summary>Tests if any element of the list satisfies the given predicate.</summary>
        ///
        /// <remarks>The predicate is applied to the elements of the input list. If any application
        /// returns true then the overall result is true and no further elements are tested.
        /// Otherwise, false is returned.</remarks>
        ///
        /// <param name="predicate">The function to test the input elements.</param>
        ///
        /// <returns>True if any result from <c>predicate</c> is true.</returns>
        member inline list.exists predicate = List.exists predicate list

        /// <summary>Returns a new collection containing only the elements of the collection
        /// for which the given predicate returns "true".</summary>
        /// <param name="predicate">The function to test the input elements.</param>
        /// <returns>An list containing the elements for which the given predicate returns true.</returns>
        member inline list.filter predicate = List.filter predicate list

        /// <summary>Returns the first element for which the given function returns 'true'.
        /// Raise <c>KeyNotFoundException</c> if no such element exists.</summary>
        ///
        /// <param name="predicate">The function to test the input elements.</param>
        ///
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">Thrown if <c>predicate</c>
        /// never returns true.</exception>
        ///
        /// <returns>The first element for which <c>predicate</c> returns true.</returns>
        member inline list.find predicate = List.find predicate list

        /// <summary>Returns the index of the first element in the list
        /// that satisfies the given predicate. Raise <c>KeyNotFoundException</c> if
        /// none of the elements satisy the predicate.</summary>
        ///
        /// <param name="predicate">The function to test the input elements.</param>
        ///
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">Thrown if <c>predicate</c>
        /// never returns true.</exception>
        ///
        /// <returns>The index of the first element in the list that satisfies the given predicate.</returns>
        member inline list.findIndex predicate = List.findIndex predicate list

        /// <summary>Tests if all elements of the list satisfy the given predicate.</summary>
        ///
        /// <remarks>The predicate is applied to the elements of the input collection. If any application
        /// returns false then the overall result is false and no further elements are tested.
        /// Otherwise, true is returned.</remarks>
        ///
        /// <param name="predicate">The function to test the input elements.</param>
        ///
        /// <returns>True if all of the list elements satisfy the predicate.</returns>
        member inline list.forall predicate = List.forall predicate list

        /// <summary>Applies a function to each element of the collection, threading an accumulator argument
        /// through the computation. If the input function is <c>f</c> and the elements are <c>i0...iN</c> then computes
        /// <c>f (... (f s i0)...) iN</c></summary>
        ///
        /// <param name="folder">The function to update the state given the input elements.</param>
        /// <param name="state">The initial state.</param>
        ///
        /// <returns>The final state.</returns>
        member inline list.fold(state:'State, folder:('State -> 'T -> 'State)) = List.fold folder state list

        /// <summary>Applies a function to each element of the list, threading an accumulator argument
        /// through the computation. If the input function is <c>f</c> and the elements are <c>i0...iN</c> then computes
        /// <c>f i0 (...(f iN s))</c></summary>
        ///
        /// <param name="folder">The function to update the state given the input elements.</param>
        /// <param name="state">The initial state.</param>
        ///
        /// <returns>The final state.</returns>
        member inline list.foldBack(folder:('T -> 'State -> 'State), state: 'State) = List.foldBack folder list state

        /// <summary>Applies the given function to each element of the list.</summary>
        ///
        /// <param name="action">The function to apply.</param>
        member inline list.iter action = List.iter action list

        /// <summary>Applies the given function to each element of the list. The integer passed to the
        /// function indicates the index of element.</summary>
        ///
        /// <param name="action">The function to apply to each index and element.</param>
        member inline list.iteri action = List.iteri action list

        /// <summary>Returns the length of an list. You can also use property source.Length.</summary>
        ///
        /// <returns>The length of the list.</returns>
        member inline list.length = List.length list

        /// <summary>Builds a new list whose elements are the results of applying the given function
        /// to each of the elements of the list.</summary>
        ///
        /// <param name="mapping">The function to transform elements of the list.</param>
        ///
        /// <returns>The list of transformed elements.</returns>
        member inline list.map mapping = List.map mapping list

        /// <summary>Builds a new list whose elements are the results of applying the given function
        /// to each of the elements of the list. The integer index passed to the
        /// function indicates the index of element being transformed.</summary>
        ///
        /// <param name="mapping">The function to transform elements and their indices.</param>
        ///
        /// <returns>The list of transformed elements.</returns>
        member inline list.mapi mapping = List.mapi mapping list

        /// <summary>Splits the collection into two collections, containing the
        /// elements for which the given predicate returns "true" and "false"
        /// respectively.</summary>
        ///
        /// <param name="predicate">The function to test the input elements.</param>
        ///
        /// <returns>A pair of lists. The first containing the elements the predicate evaluated to true,
        /// and the second containing those evaluated to false.</returns>
        member inline list.partition predicate = List.partition predicate list

        /// <summary>Returns an list with all elements permuted according to the
        /// specified permutation.</summary>
        ///
        /// <param name="indexMap">The function that maps input indices to output indices.</param>
        ///
        /// <returns>The output list.</returns>
        member inline list.permute indexMap = List.permute indexMap list

        /// <summary>Applies a function to each element of the list, threading an accumulator argument
        /// through the computation. If the input function is <c>f</c> and the elements are <c>i0...iN</c>
        /// then computes <c>f (... (f i0 i1)...) iN</c>.
        /// Raises ArgumentException if the list has size zero.</summary>
        ///
        /// <param name="reduction">The function to reduce a pair of elements to a single element.</param>
        ///
        /// <exception cref="System.ArgumentException">Thrown when the input list is empty.</exception>
        ///
        /// <returns>The final result of the redcutions.</returns>
        member inline list.reduce reduction = List.reduce reduction list

        /// <summary>Applies a function to each element of the list, threading an accumulator argument
        /// through the computation. If the input function is <c>f</c> and the elements are <c>i0...iN</c>
        /// then computes <c>f i0 (...(f iN-1 iN))</c>.
        /// Raises ArgumentException if the list has size zero.</summary>
        ///
        /// <param name="reduction">The function to reduce a pair of elements to a single element.</param>
        ///
        /// <exception cref="System.ArgumentException">Thrown when the input list is empty.</exception>
        ///
        /// <returns>The final result of the reductions.</returns>
        member inline list.reduceBack reduction = List.reduceBack reduction list

        /// <summary>Returns a new list with the elements in reverse order.</summary>
        ///
        /// <returns>The reversed list.</returns>
        member inline list.reverse() = List.rev list

        /// <summary>Like <c>fold</c>, but return the intermediary and final results.</summary>
        ///
        /// <param name="folder">The function to update the state given the input elements.</param>
        /// <param name="state">The initial state.</param>
        ///
        /// <returns>The list of state values.</returns>
        member inline list.scan(state:'State, folder:('State -> 'T -> 'State)) = List.scan folder state list

        /// <summary>Like <c>foldBack</c>, but return both the intermediary and final results.</summary>
        ///
        /// <param name="folder">The function to update the state given the input elements.</param>
        /// <param name="state">The initial state.</param>
        ///
        /// <returns>The list of state values.</returns>
        member inline list.scanBack(folder:('T -> 'State -> 'State), state:'State) = List.scanBack folder list state

        /// <summary>Sorts the elements of an list, using the given projection for the keys and returning a new list.
        /// Elements are compared using Operators.compare.</summary>
        ///
        /// <remarks>This is not a stable sort, i.e. the original order of equal elements is not necessarily preserved.
        /// For a stable sort, consider using Seq.sort.</remarks>
        ///
        /// <param name="projection">The function to transform list elements into the type that is compared.</param>
        ///
        /// <returns>The sorted list.</returns>
        [<Extension>]
        member inline list.sortBy(projection:('T -> 'Key)) = List.sortBy projection list

        /// <summary>Sorts the elements of an list, using the given comparison function as the order, returning a new list.</summary>
        ///
        /// <remarks>This is not a stable sort, i.e. the original order of equal elements is not necessarily preserved.
        /// For a stable sort, consider using Seq.sort.</remarks>
        ///
        /// <param name="comparer">The function to compare pairs of list elements.</param>
        ///
        /// <returns>The sorted list.</returns>
        member inline list.sortWith(comparer) = List.sortWith comparer list

        /// <summary>Views the given list as a sequence.</summary>
        ///
        /// <returns>The sequence of list elements.</returns>
        member inline list.toSeq() = List.toSeq list

        /// <summary>Returns the first element for which the given function returns <c>true</c>.
        /// Return <c>None</c> if no such element exists.</summary>
        ///
        /// <param name="predicate">The function to test the input elements.</param>
        ///
        /// <returns>The first element that satisfies the predicate, or None.</returns>
        member inline list.tryFind predicate = List.tryFind predicate list

        /// <summary>Returns the index of the first element in the list
        /// that satisfies the given predicate.</summary>
        ///
        /// <param name="predicate">The function to test the input elements.</param>
        ///
        /// <returns>The index of the first element that satisfies the predicate, or None.</returns>
        member inline list.tryFindIndex predicate = List.tryFindIndex predicate list

        /// <summary>Combines the two lists into an list of pairs. The two lists must have equal lengths, otherwise an <c>ArgumentException</c> is
        /// raised.</summary>
        ///
        /// <param name="list2">The second input list.</param>
        ///
        /// <exception cref="System.ArgumentException">Thrown when the input lists differ in length.</exception>
        ///
        /// <returns>The list of tupled elements.</returns>
        member inline list.zip(list2: 'T2 list) = List.zip list list2

        /// <summary>Combines three lists into an list of pairs. The three lists must have equal lengths, otherwise an <c>ArgumentException</c> is
        /// raised.</summary>
        ///
        /// <param name="list2">The second input list.</param>
        /// <param name="list3">The third input list.</param>
        ///
        /// <exception cref="System.ArgumentException">Thrown when the input lists differ in length.</exception>
        ///
        /// <returns>The list of tupled elements.</returns>
        member inline list.zip3(list2:'T2 list, list3: 'T3 list) = List.zip3 list list2 list3

        /// <summary>Applies a key-generating function to each element of a list and returns a list yielding unique
        /// keys and their number of occurrences in the original list.</summary>
        ///
        /// <param name="projection">A function transforming each item of the input list into a key to be
        /// compared against the others.</param>
        ///
        /// <returns>The result list.</returns>
        member inline list.countBy(projection:('T -> 'Key)) = List.countBy projection list

        /// <summary>Returns a list that contains no duplicate entries according to the
        /// generic hash and equality comparisons on the keys returned by the given key-generating function.
        /// If an element occurs multiple times in the list then the later occurrences are discarded.</summary>
        ///
        /// <param name="projection">A function transforming the list items into comparable keys.</param>
        ///
        /// <returns>The result list.</returns>
        member inline list.distinctBy(projection:('T -> 'Key)) = List.distinctBy projection list

        /// <summary>Applies a key-generating function to each element of a list and yields a list of
        /// unique keys. Each unique key contains a list of all elements that match
        /// to this key.</summary>
        ///
        /// <param name="projection">A function that transforms an element of the list into a comparable key.</param>
        ///
        /// <returns>The result list.</returns>
        member inline list.groupBy(projection:('T -> 'Key)) = List.groupBy projection list

        /// <summary>Returns a list of each element in the input list and its predecessor, with the
        /// exception of the first element which is only returned as the predecessor of the second element.</summary>
        ///
        /// <returns>The result list.</returns>
        member inline list.pairwise() = List.pairwise list

        /// <summary>Returns the list after removing the first N elements.</summary>
        ///
        /// <param name="count">The number of elements to skip.</param>
        ///
        /// <returns>The list after removing the first N elements.</returns>
        ///
        /// <exception cref="System.ArgumentException">Thrown when count is negative or exceeds the number of
        /// elements in the list.</exception>
        member inline list.skip(count:int) = List.skip count list

        /// <summary>Bypasses elements in a list while the given predicate returns <c>true</c>, and then returns
        /// the remaining elements of the list.</summary>
        ///
        /// <param name="predicate">A function that evaluates an element of the list to a boolean value.</param>
        ///
        /// <returns>The result list.</returns>
        member inline list.skipWhile(predicate:('T -> bool)) = List.skipWhile predicate list

        /// <summary>Returns the first N elements of the list.</summary>
        ///
        /// <remarks>Throws <c>InvalidOperationException</c>
        /// if the count exceeds the number of elements in the list. <c>List.truncate</c>
        /// returns as many items as the list contains instead of throwing an exception.</remarks>
        ///
        /// <param name="count">The number of items to take.</param>
        ///
        /// <returns>The result list.</returns>
        ///
        /// <exception cref="System.ArgumentException">Thrown when the input list is empty.</exception>
        /// <exception cref="System.InvalidOperationException">Thrown when count exceeds the number of elements
        /// in the list.</exception>
        member inline list.take(count:int) = List.take count list

        /// <summary>Returns a list that contains all elements of the original list while the
        /// given predicate returns <c>true</c>, and then returns no further elements.</summary>
        ///
        /// <param name="predicate">A function that evaluates to false when no more items should be returned.</param>
        ///
        /// <returns>The result list.</returns>
        member inline list.takeWhile(predicate:('T -> bool)) = List.takeWhile predicate list

        /// <summary>Returns at most N elements in a new list.</summary>
        ///
        /// <param name="count">The maximum number of items to return.</param>
        ///
        /// <returns>The result list.</returns>
        ///
        /// <exception cref="System.ArgumentException">Thrown when the count is negative.</exception>
        member inline list.truncate(count:int) = List.truncate count list

        /// <summary>Returns a new list containing only the elements of the list
        /// for which the given predicate returns "true"</summary>
        ///
        /// <param name="predicate">The function to test the input elements.</param>
        ///
        /// <returns>A list containing only the elements that satisfy the predicate.</returns>
        member inline list.where(predicate:('T -> bool)) = List.where predicate list

        /// <summary>Returns a list of sliding windows containing elements drawn from the input
        /// list. Each window is returned as a fresh list.</summary>
        ///
        /// <param name="windowSize">The number of elements in each window.</param>
        ///
        /// <returns>The result list.</returns>
        ///
        /// <exception cref="System.ArgumentException">Thrown when windowSize is not positive.</exception>
        member inline list.windowed(windowSize:int) = List.windowed windowSize list

        /// <summary>Builds a new collection whose elements are the corresponding elements of the input collection
        /// paired with the integer index (from 0) of each element.</summary>
        ///
        /// <returns>The result list.</returns>
        member inline list.indexed() = List.indexed list

        /// <summary>Returns the only element of the list.</summary>
        ///
        /// <returns>The only element of the list.</returns>
        ///
        /// <exception cref="System.ArgumentException">Thrown when the input does not have precisely one element.</exception>
        member inline list.exactlyOne() = List.exactlyOne list

        /// <summary>Sorts the given list in descending order using keys given by the given projection. Keys are compared using Operators.compare.</summary>
        ///
        /// <remarks>This is a stable sort, i.e. the original order of equal elements is preserved.</remarks>
        /// <param name="projection">The function to transform the list elements into the type to be compared.</param>
        /// <returns>The sorted list.</returns>
        member inline list.sortByDescending(projection:('T -> 'Key)) = List.sortByDescending projection list

        /// <summary>Returns the index of the last element in the list
        /// that satisfies the given predicate.
        /// Return <c>None</c> if no such element exists.</summary>
        ///
        /// <param name="predicate">The function to test the input elements.</param>
        ///
        /// <returns>The index of the last element for which the predicate returns true, or None if
        /// every element evaluates to false.</returns>
        member inline list.tryFindIndexBack(predicate:('T -> bool)) = List.tryFindIndexBack predicate list

        /// <summary>Returns the last element for which the given function returns <c>true.</c>.
        /// Return <c>None</c> if no such element exists.</summary>
        ///
        /// <param name="predicate">The function to test the input elements.</param>
        ///
        /// <returns>The last element for which the predicate returns true, or None if
        /// every element evaluates to false.</returns>
        member inline list.tryFindBack(predicate:('T -> bool)) = List.tryFindBack predicate list

        /// <summary>Tries to find the nth element in the list.
        /// Returns <c>None</c> if index is negative or the list does not contain enough elements.</summary>
        ///
        /// <param name="index">The index to retrieve.</param>
        ///
        /// <returns>The value at the given index or <c>None</c>.</returns>
        member inline list.tryItem(index:int) = List.tryItem index list

        /// <summary>Returns the list after removing the first element.</summary>
        ///
        /// <exception cref="System.ArgumentException">Thrown when the list is empty.</exception>
        ///
        /// <returns>The list after removing the first element.</returns>
        member inline list.tail() = List.tail list

        /// <summary>Returns the first element of the list.</summary>
        ///
        /// <exception cref="System.ArgumentException">Thrown when the list is empty.</exception>
        ///
        /// <returns>The first element of the list.</returns>
        member inline list.head() = List.head list

        /// <summary>Returns the last element of the list.</summary>
        ///
        /// <returns>The last element of the list.</returns>
        ///
        /// <exception cref="System.ArgumentException">Thrown when the input does not have any elements.</exception>
        member inline list.last() = List.last list

        /// <summary>Returns the last element of the list.
        /// Return <c>None</c> if no such element exists.</summary>
        ///
        /// <returns>The last element of the list or None.</returns>
        member inline list.tryLast() = List.tryLast list

        /// <summary>Returns the first element of the list, or
        /// <c>None</c> if the list is empty.</summary>
        ///
        /// <returns>The first element of the list or None.</returns>
        member inline list.tryHead() = List.tryHead list

        /// <summary>Returns the transpose of the given sequence of lists.</summary>
        ///
        /// <param name="lists">The input sequence of lists.</param>
        ///
        /// <returns>The transposed list.</returns>
        ///
        /// <exception cref="T:System.ArgumentNullException">Thrown when the input sequence is null.</exception>
        /// <exception cref="T:System.ArgumentException">Thrown when the input lists differ in length.</exception>
        [<Extension>]
        static member inline transpose(lists: seq<'T list>) = List.transpose lists

[<Extension>]
/// <summary>Fluent extension operations on lists which require constrained types.</summary>
type ListExtensionsConstrained =

    /// <summary>Returns a new list that contains all pairings of elements from two lists.</summary>
    ///
    /// <param name="list2">The second input list.</param>
    ///
    /// <returns>The resulting list of pairs.</returns>
    [<Extension>]
    static member inline allPairs(list:'T list, list2:'U list) = List.allPairs list list2

    /// <summary>Return a new list with a new item inserted before the given index.</summary>
    ///
    /// <param name="index">The index where the item should be inserted.</param>
    /// <param name="value">The value to insert.</param>
    /// <exception cref="System.ArgumentException">Thrown when index is below 0 or greater than source.Length.</exception>
    ///
    /// <returns>The result list.</returns>
    [<Extension>]
    static member inline insertAt(list:'T list, index, value) = List.insertAt index value list

    /// <summary>Return a new list with new items inserted before the given index.</summary>
    ///
    /// <param name="index">The index where the items should be inserted.</param>
    /// <param name="values">The values to insert.</param>
    /// <exception cref="System.ArgumentException">Thrown when index is below 0 or greater than source.Length.</exception>
    ///
    /// <returns>The result list.</returns>
    [<Extension>]
    static member inline insertManyAt(list:'T list, index, values) = List.insertManyAt index values list

    /// <summary>Return a new list with the item at a given index removed.</summary>
    ///
    /// <param name="index">The index of the item to be removed.</param>
    /// <exception cref="System.ArgumentException">Thrown when index is outside 0..source.Length - 1</exception>
    ///
    /// <returns>The result list.</returns>
    [<Extension>]
    static member inline removeAt(list:'T list, index) = List.removeAt index list

    /// <summary>Return a new list with the number of items starting at a given index removed.</summary>
    ///
    /// <param name="index">The index of the item to be removed.</param>
    /// <param name="count">The number of items to remove.</param>
    /// <exception cref="System.ArgumentException">Thrown when index is outside 0..source.Length - count</exception>
    ///
    /// <returns>The result list.</returns>
    [<Extension>]
    static member inline removeManyAt(list:'T list, index, count) = List.removeManyAt index count list

    /// <summary>Splits a list into two lists, at the given index.</summary>
    ///
    /// <param name="index">The index at which the list is split.</param>
    /// <exception cref="System.InvalidOperationException">Thrown when split index exceeds the number of elements in the list.</exception>
    ///
    /// <returns>The two split lists.</returns>
    [<Extension>]
    static member inline splitAt(list:'T list, index) = List.splitAt index list

    /// <summary>Returns the only element of the list or <c>None</c> if it is empty or contains more than one element.</summary>
    ///
    /// <returns>The only element of the list or None.</returns>
    [<Extension>]
    static member inline tryExactlyOne(list:'T list) = List.tryExactlyOne list

    /// <summary>Return a new list with the item at a given index set to the new value.</summary>
    ///
    /// <param name="index">The index of the item to be replaced.</param>
    /// <param name="value">The new value.</param>
    ///
    /// <exception cref="System.ArgumentException">Thrown when index is outside 0..source.Length - 1</exception>
    ///
    /// <returns>The result list.</returns>
    [<Extension>]
    static member inline updateAt(list:'T list, index, value) = List.updateAt index value list

    /// <summary>Returns the average of the elements in the list.</summary>
    ///
    /// <param name="list">The input list.</param>
    ///
    /// <exception cref="System.ArgumentException">Thrown when <c>list</c> is empty.</exception>
    ///
    /// <returns>The average of the elements in the list.</returns>
    [<Extension>]
    static member inline average(list:'T list) = List.average list

    /// <summary>Returns the average of the elements generated by applying the function to each element of the list.</summary>
    ///
    /// <param name="projection">The function to transform the list elements before averaging.</param>
    /// <param name="list">The input list.</param>
    ///
    /// <exception cref="System.ArgumentException">Thrown when <c>list</c> is empty.</exception>
    ///
    /// <returns>The computed average.</returns>
    [<Extension>]
    static member inline averageBy(list:'T list, projection:('T -> 'U)) = List.averageBy projection list

    /// <summary>Returns a list that contains no duplicate entries according to generic hash and
    /// equality comparisons on the entries.
    /// If an element occurs multiple times in the list then the later occurrences are discarded.</summary>
    ///
    /// <param name="list">The input list.</param>
    ///
    /// <returns>The result list.</returns>
    [<Extension>]
    static member inline distinct(list:'T list) = List.distinct list

    /// <summary>Returns the greatest of all elements of the list, compared via Operators.max on the function result.</summary>
    ///
    /// <remarks>Throws ArgumentException for empty arrays.</remarks>
    ///
    /// <param name="list">The input list.</param>
    ///
    /// <exception cref="System.ArgumentException">Thrown when the input list is empty.</exception>
    ///
    /// <returns>The maximum element.</returns>
    [<Extension>]
    static member inline max(list:'T list) = List.max list

    /// <summary>Returns the greatest of all elements of the list, compared via Operators.max on the function result.</summary>
    ///
    /// <remarks>Throws ArgumentException for empty arrays.</remarks>
    ///
    /// <param name="projection">The function to transform the elements into a type supporting comparison.</param>
    /// <param name="list">The input list.</param>
    ///
    /// <exception cref="System.ArgumentException">Thrown when the input list is empty.</exception>
    ///
    /// <returns>The maximum element.</returns>
    [<Extension>]
    static member inline maxBy(list:'T list, projection: 'T -> 'Key) = List.maxBy projection list

    /// <summary>Returns the lowest of all elements of the list, compared via Operators.min.</summary>
    ///
    /// <remarks>Throws ArgumentException for empty arrays</remarks>
    ///
    /// <param name="list">The input list.</param>
    ///
    /// <exception cref="System.ArgumentException">Thrown when the input list is empty.</exception>
    ///
    /// <returns>The minimum element.</returns>
    [<Extension>]
    static member inline min(list:'T list) = List.min list

    /// <summary>Returns the lowest of all elements of the list, compared via Operators.min on the function result.</summary>
    ///
    /// <remarks>Throws ArgumentException for empty arrays.</remarks>
    ///
    /// <param name="projection">The function to transform the elements into a type supporting comparison.</param>
    /// <param name="list">The input list.</param>
    ///
    /// <exception cref="System.ArgumentException">Thrown when the input list is empty.</exception>
    ///
    /// <returns>The minimum element.</returns>
    [<Extension>]
    static member inline minBy(list:'T list, projection: 'T -> 'Key) = List.minBy projection list

    /// <summary>Sorts the elements of an list, returning a new list. Elements are compared using Operators.compare. </summary>
    ///
    /// <remarks>This is not a stable sort, i.e. the original order of equal elements is not necessarily preserved.
    /// For a stable sort, consider using Seq.sort.</remarks>
    /// <param name="list">The input list.</param>
    ///
    /// <returns>The sorted list.</returns>
    [<Extension>]
    static member inline sort(list:'T list) = List.sort list

    /// <summary>Returns the sum of the elements in the list.</summary>
    ///
    /// <param name="list">The input list.</param>
    ///
    /// <returns>The resulting sum.</returns>
    [<Extension>]
    static member inline sum(list: 'T list) = List.sum list

    /// <summary>Returns the sum of the results generated by applying the function to each element of the list.</summary>
    ///
    /// <param name="projection">The function to transform the list elements into the type to be summed.</param>
    /// <param name="list">The input list.</param>
    ///
    /// <returns>The resulting sum.</returns>
    [<Extension>]
    static member inline sumBy(list: 'T list, projection:('T -> 'U)) = List.sumBy projection list

    /// <summary>Splits an list of pairs into two arrays.</summary>
    ///
    /// <param name="list">The input list.</param>
    ///
    /// <returns>The two arrays.</returns>
    [<Extension>]
    static member inline unzip(list) = List.unzip list

    /// <summary>Splits an list of triples into three arrays.</summary>
    ///
    /// <param name="list">The input list.</param>
    ///
    /// <returns>The tuple of three arrays.</returns>
    [<Extension>]
    static member inline unzip3(list) = List.unzip3 list

#if TODO
// Array.Parallel.map
#endif

/// <summary>Fluent extension operations on 2D arrays.</summary>
[<AutoOpen>]
module Array2DExtensions =

    type ``[,]``<'T> with

        /// <summary>Fetches the base-index for the first dimension of the array.</summary>
        ///
        /// <returns>The base-index of the first dimension of the array.</returns>
        member inline array.base1 = Array2D.base1 array

        /// <summary>Fetches the base-index for the second dimension of the array.</summary>
        ///
        /// <returns>The base-index of the second dimension of the array.</returns>
        member inline array.base2 = Array2D.base2 array

        /// <summary>Returns the length of an array in the first dimension.</summary>
        ///
        /// <returns>The length of the array in the first dimension.</returns>
        member inline array.length1 = Array2D.length1 array

        /// <summary>Returns the length of an array in the second dimension.</summary>
        ///
        /// <returns>The length of the array in the second dimension.</returns>
        member inline array.length2 = Array2D.length2 array

        /// <summary>Builds a new array whose elements are the same as the input array but
        /// where a non-zero-based input array generates a corresponding zero-based
        /// output array.</summary>
        ///
        /// <returns>The zero-based output array.</returns>
        member inline array.rebase() = Array2D.rebase array

        /// <summary>Builds a new array whose elements are the results of applying the given function
        /// to each of the elements of the array.</summary>
        ///
        /// <remarks>For non-zero-based arrays the basing on an input array will be propogated to the output
        /// array.</remarks>
        ///
        /// <param name="mapping">A function that is applied to transform each item of the input array.</param>
        ///
        /// <returns>An array whose elements have been transformed by the given mapping.</returns>
        member inline array.map(mapping:('T -> 'U)) = Array2D.map mapping array

        /// <summary>Builds a new array whose elements are the results of applying the given function
        /// to each of the elements of the array. The integer indices passed to the
        /// function indicates the element being transformed.</summary>
        ///
        /// <remarks>For non-zero-based arrays the basing on an input array will be propagated to the output
        /// array.</remarks>
        ///
        /// <param name="mapping">A function that is applied to transform each element of the array.  The two integers
        /// provide the index of the element.</param>
        ///
        /// <returns>An array whose elements have been transformed by the given mapping.</returns>
        member inline array.mapi(mapping:(int -> int -> 'T -> 'U)) = Array2D.mapi mapping array

        /// <summary>Applies the given function to each element of the array.</summary>
        ///
        /// <param name="action">A function to apply to each element of the array.</param>
        member inline array.iter(action:('T -> unit)) = Array2D.iter action array

        /// <summary>Applies the given function to each element of the array.  The integer indices passed to the
        /// function indicates the index of element.</summary>
        ///
        /// <param name="action">A function to apply to each element of the array with the indices available as an argument.</param>
        member inline array.iteri(action:(int -> int -> 'T -> unit)) = Array2D.iteri action array

        /// <summary>Builds a new array whose elements are the same as the input array.</summary>
        ///
        /// <remarks>For non-zero-based arrays the basing on an input array will be propogated to the output
        /// array.</remarks>
        ///
        /// <returns>A copy of the input array.</returns>
        member inline array.copy() = Array2D.copy array

/// <summary>Fluent extension operations on 3D arrays.</summary>
[<AutoOpen>]
module Array3DExtensions =

    type ``[,,]``<'T> with

        /// <summary>Applies the given function to each element of the array.</summary>
        ///
        /// <param name="action">The function to apply to each element of the array.</param>
        member inline array.iter(action:('T -> unit)) = Array3D.iter action array

        /// <summary>Applies the given function to each element of the array. The integer indicies passed to the
        /// function indicates the index of element.</summary>
        ///
        /// <param name="action">The function to apply to each element of the array.</param>
        member inline array.iteri(action:(int -> int -> int -> 'T -> unit)) = Array3D.iteri action array

        /// <summary>Returns the length of an array in the first dimension  </summary>
        ///
        /// <returns>The length of the array in the first dimension.</returns>
        member inline array.length1 = Array3D.length1 array

        /// <summary>Returns the length of an array in the second dimension.</summary>
        ///
        /// <returns>The length of the array in the second dimension.</returns>
        member inline array.length2 = Array3D.length2 array

        /// <summary>Returns the length of an array in the third dimension.</summary>
        ///
        /// <returns>The length of the array in the third dimension.</returns>
        member inline array.length3 = Array3D.length3 array

        /// <summary>Builds a new array whose elements are the results of applying the given function
        /// to each of the elements of the array.</summary>
        ///
        /// <remarks>For non-zero-based arrays the basing on an input array will be propogated to the output
        /// array.</remarks>
        ///
        /// <param name="mapping">The function to transform each element of the array.</param>
        ///
        /// <returns>The array created from the transformed elements.</returns>
        member inline array.map(mapping:('T -> 'U) )  = Array3D.map mapping array

        /// <summary>Builds a new array whose elements are the results of applying the given function
        /// to each of the elements of the array. The integer indices passed to the
        /// function indicates the element being transformed.</summary>
        ///
        /// <remarks>For non-zero-based arrays the basing on an input array will be propogated to the output
        /// array.</remarks>
        ///
        /// <param name="mapping">The function to transform the elements at each index in the array.</param>
        ///
        /// <returns>The array created from the transformed elements.</returns>
        member inline array.mapi(mapping:(int -> int -> int -> 'T -> 'U) ) = Array3D.mapi mapping array

/// <summary>Fluent extension operations on 4D arrays.</summary>
[<AutoOpen>]
module Array4DExtensions =

    type ``[,,,]``<'T> with

        /// <summary>Returns the length of an array in the first dimension  </summary>
        ///
        /// <returns>The length of the array in the first dimension.</returns>
        member inline array.length1 = Array4D.length1 array

        /// <summary>Returns the length of an array in the second dimension.</summary>
        ///
        /// <returns>The length of the array in the second dimension.</returns>
        member inline array.length2 = Array4D.length2 array

        /// <summary>Returns the length of an array in the third dimension.</summary>
        ///
        /// <returns>The length of the array in the third dimension.</returns>
        member inline array.length3 = Array4D.length3 array

        /// <summary>Returns the length of an array in the fourth dimension.</summary>
        ///
        /// <returns>The length of the array in the fourth dimension.</returns>
        member inline array.length4 = Array4D.length4 array

/// <summary>Fluent extension operations on strings.</summary>
[<AutoOpen>]
module StringExtensions =

    type System.String with

        /// <summary>Applies the function <c>action</c> to each character in the string.</summary>
        ///
        /// <param name="action">The function to be applied to each character of the string.</param>
        member inline str.iter(action:(char -> unit)) = String.iter action str

        /// <summary>Applies the function <c>action</c> to the index of each character in the string and the
        /// character itself.</summary>
        ///
        /// <param name="action">The function to apply to each character and index of the string.</param>
        member inline str.iteri(action:(int -> char -> unit)) = String.iteri action str

        /// <summary>Builds a new string whose characters are the results of applying the function <c>mapping</c>
        /// to each of the characters of the input string.</summary>
        /// <param name="mapping">The function to apply to the characters of the string.</param>
        ///
        /// <returns>The resulting string.</returns>
        member inline str.map(mapping:(char -> char)) = String.map mapping str

        /// <summary>Builds a new string whose characters are the results of applying the function <c>mapping</c>
        /// to each character and index of the input string.</summary>
        ///
        /// <param name="mapping">The function to apply to each character and index of the string.</param>
        ///
        /// <returns>The resulting string.</returns>
        member inline str.mapi(mapping:(int -> char -> char)) = String.mapi mapping str

        /// <summary>Builds a new string whose characters are the results of applying the function <c>mapping</c>
        /// to each of the characters of the input string and concatenating the resulting
        /// strings.</summary>
        ///
        /// <param name="mapping">The function to produce a string from each character of the input string.</param>
        ///
        /// <returns>The concatenated string.</returns>
        member inline str.collect(mapping:(char -> string)) = String.collect mapping str

        /// <summary>Tests if all characters in the string satisfy the given predicate.</summary>
        ///
        /// <param name="predicate">The function to test each character of the string.</param>
        ///
        /// <returns>True if all characters return true for the predicate and false otherwise.</returns>
        member inline str.forall(predicate:(char -> bool)) = String.forall predicate str

        /// <summary>Tests if any character of the string satisfies the given predicate.</summary>
        ///
        /// <param name="predicate">The function to test each character of the string.</param>
        ///
        /// <returns>True if any character returns true for the predicate and false otherwise.</returns>
        member inline str.exists(predicate:(char -> bool)) = String.exists predicate str

        /// <summary>Returns a string by concatenating <c>count</c> instances of <c>str</c>.</summary>
        ///
        /// <param name="count">The number of copies of the input string will be copied.</param>
        ///
        /// <returns>The concatenated string.</returns>
        ///
        /// <exception cref="System.ArgumentException">Thrown when <c>count</c> is negative.</exception>
        member inline str.replicate(count:int) = String.replicate count str

        /// <summary>Returns the length of the string.</summary>
        ///
        /// <returns>The number of characters in the string.</returns>
        member inline str.length = String.length str

/// <summary>Fluent extension operations on options.</summary>
[<AutoOpen>]
module OptionExtensions =

    type Option<'T> with

        /// <summary>Evaluates to <c>match inp with None -> 0 | Some _ -> 1</c>.</summary>
        ///
        /// <returns>A zero if the option is None, a one otherwise.</returns>
        member inline opt.count = Option.count opt

        /// <summary><c>fold f s inp</c> evaluates to <c>match inp with None -> s | Some x -> f s x</c>.</summary>
        ///
        /// <param name="folder">A function to update the state data when given a value from an option.</param>
        /// <param name="state">The initial state.</param>
        ///
        /// <returns>The original state if the option is None, otherwise it returns the updated state with the folder
        /// and the option value.</returns>
        member inline opt.fold(state:'State , folder:('State -> 'T -> 'State)) = Option.fold folder state opt

        /// <summary><c>fold f inp s</c> evaluates to <c>match inp with None -> s | Some x -> f x s</c>.</summary>
        ///
        /// <param name="folder">A function to update the state data when given a value from an option.</param>
        /// <param name="state">The initial state.</param>
        ///
        /// <returns>The original state if the option is None, otherwise it returns the updated state with the folder
        /// and the option value.</returns>
        member inline opt.foldBack(folder:('T -> 'State -> 'State), state:'State) = Option.foldBack folder opt state

        /// <summary><c>exists p inp</c> evaluates to <c>match inp with None -> false | Some x -> p x</c>.</summary>
        ///
        /// <param name="predicate">A function that evaluates to a boolean when given a value from the option type.</param>
        ///
        /// <returns>False if the option is None, otherwise it returns the result of applying the predicate
        /// to the option value.</returns>
        member inline opt.exists(predicate:('T -> bool)) = Option.exists predicate opt

        /// <summary><c>forall p inp</c> evaluates to <c>match inp with None -> true | Some x -> p x</c>.</summary>
        ///
        /// <param name="predicate">A function that evaluates to a boolean when given a value from the option type.</param>
        ///
        /// <returns>True if the option is None, otherwise it returns the result of applying the predicate
        /// to the option value.</returns>
        member inline opt.forall(predicate:('T -> bool)) = Option.forall predicate opt

        /// <summary><c>iter f inp</c> executes <c>match inp with None -> () | Some x -> f x</c>.</summary>
        ///
        /// <param name="action">A function to apply to the option value.</param>
        ///
        /// <returns>Unit if the option is None, otherwise it returns the result of applying the predicate
        /// to the option value.</returns>
        member inline opt.iter(action:('T -> unit)) = Option.iter action opt

        /// <summary><c>map f inp</c> evaluates to <c>match inp with None -> None | Some x -> Some (f x)</c>.</summary>
        ///
        /// <param name="mapping">A function to apply to the option value.</param>
        ///
        /// <returns>An option of the input value after applying the mapping function, or None if the input is None.</returns>
        member inline opt.map(mapping:('T -> 'U)) = Option.map mapping opt

        /// <summary><c>bind f inp</c> evaluates to <c>match inp with None -> None | Some x -> f x</c></summary>
        ///
        /// <param name="binder">A function that takes the value of type T from an option and transforms it into
        /// an option containing a value of type U.</param>
        ///
        /// <returns>An option of the output type of the binder.</returns>
        member inline opt.bind(binder:('T -> 'U option)) = Option.bind binder opt

        /// <summary>Convert the option to an array of length 0 or 1.</summary>
        ///
        /// <returns>The result array.</returns>
        member inline opt.toArray() = Option.toArray opt

        /// <summary>Convert the option to a list of length 0 or 1.</summary>
        ///
        /// <returns>The result list.</returns>
        member inline opt.toList() = Option.toList opt

        /// <summary><c>filter f inp</c> evaluates to <c>match inp with None -> None | Some x -> if f x then Some x else None</c>.</summary>
        ///
        /// <param name="predicate">A function that evaluates whether the value contained in the option should remain, or be filtered out.</param>
        ///
        /// <returns>The input if the predicate evaluates to true; otherwise, None.</returns>
        member inline opt.filter(predicate:('T -> bool)) = Option.filter predicate opt

[<Extension>]
/// <summary>Fluent extension operations on options which require constrained types.</summary>
type OptionExtensionsConstrained =

    /// <summary>Convert the option to a Nullable value.</summary>
    ///
    /// <returns>The result value.</returns>
    [<Extension>]
    static member inline toNullable(opt: option<'T>) = Option.toNullable opt

    /// <summary>Convert an option to a potentially null value.</summary>
    ///
    /// <returns>The result value, which is null if the input was None.</returns>
    [<Extension>]
    static member inline toObj(opt: option<'T>) = Option.toObj opt

/// <summary>Fluent extension operations on options.</summary>
[<AutoOpen>]
module NullableExtensions =

    type Nullable<'T when 'T : (new :  unit -> 'T) and 'T : struct and 'T :> ValueType > with

        /// <summary>Convert a Nullable value to an option.</summary>
        ///
        /// <returns>The result option.</returns>
        member inline value.toOption() = Option.ofNullable value


/// <summary>Fluent extension operations on observables.</summary>
[<AutoOpen>]
module ObservableExtensions =

    /// <summary>Basic operations on first class event and other observable objects.</summary>
    type IObservable<'T> with

        /// <summary>Returns an observable for the merged observations from the sources.
        /// The returned object propagates success and error values arising
        /// from either source and completes when both the sources have completed.</summary>
        ///
        /// <remarks>For each observer, the registered intermediate observing object is not
        /// thread safe. That is, observations arising from the sources must not
        /// be triggered concurrently on different threads.</remarks>
        ///
        /// <param name="obs2">The second Observable.</param>
        ///
        /// <returns>An Observable that propagates information from both sources.</returns>
        member inline obs.merge(obs2:IObservable<'T>) = Observable.merge obs obs2

        /// <summary>Returns an observable which transforms the observations of the source by the
        /// given function. The transformation function is executed once for each
        /// subscribed observer. The returned object also propagates error observations
        /// arising from the source and completes when the source completes.</summary>
        ///
        /// <param name="mapping">The function applied to observations from the source.</param>
        ///
        /// <returns>An Observable of the type specified by <c>mapping</c>.</returns>
        member inline obs.map(mapping:('T -> 'U)) = Observable.map mapping obs

        /// <summary>Returns an observable which filters the observations of the source
        /// by the given function. The observable will see only those observations
        /// for which the predicate returns true. The predicate is executed once for
        /// each subscribed observer. The returned object also propagates error
        /// observations arising from the source and completes when the source completes.</summary>
        /// <param name="predicate">The function to apply to observations to determine if it should
        /// be kept.</param>
        ///
        /// <returns>An Observable that filters observations based on <c>filter</c>.</returns>
        member inline obs.filter(predicate:('T -> bool)) = Observable.filter predicate obs

        /// <summary>Returns two observables which partition the observations of the source by
        /// the given function. The first will trigger observations for those values
        /// for which the predicate returns true. The second will trigger observations
        /// for those values where the predicate returns false. The predicate is
        /// executed once for each subscribed observer. Both also propagate all error
        /// observations arising from the source and each completes when the source
        /// completes.</summary>
        ///
        /// <param name="predicate">The function to determine which output Observable will trigger
        /// a particular observation.</param>
        ///
        /// <returns>A tuple of Observables.  The first triggers when the predicate returns true, and
        /// the second triggers when the predicate returns false.</returns>
        member inline obs.partition(predicate:('T -> bool)) = Observable.partition predicate obs

        /// <summary>Returns two observables which split the observations of the source by the
        /// given function. The first will trigger observations <c>x</c> for which the
        /// splitter returns <c>Choice1Of2 x</c>. The second will trigger observations
        /// <c>y</c> for which the splitter returns <c>Choice2Of2 y</c> The splitter is
        /// executed once for each subscribed observer. Both also propagate error
        /// observations arising from the source and each completes when the source
        /// completes.</summary>
        ///
        /// <param name="splitter">The function that takes an observation an transforms
        /// it into one of the two output Choice types.</param>
        ///
        /// <returns>A tuple of Observables.  The first triggers when <c>splitter</c> returns Choice1of2
        /// and the second triggers when <c>splitter</c> returns Choice2of2.</returns>
        member inline obs.split(splitter:('T -> Choice<'U1,'U2>)) = Observable.split splitter obs

        /// <summary>Returns an observable which chooses a projection of observations from the source
        /// using the given function. The returned object will trigger observations <c>x</c>
        /// for which the splitter returns <c>Some x</c>. The returned object also propagates
        /// all errors arising from the source and completes when the source completes.</summary>
        ///
        /// <param name="chooser">The function that returns Some for observations to be propagated
        /// and None for observations to ignore.</param>
        ///
        /// <returns>An Observable that only propagates some of the observations from the source.</returns>
        member inline obs.choose(chooser:('T -> 'U option)) = Observable.choose chooser obs

        /// <summary>Returns an observable which, for each observer, allocates an item of state
        /// and applies the given accumulating function to successive values arising from
        /// the input. The returned object will trigger observations for each computed
        /// state value, excluding the initial value. The returned object propagates
        /// all errors arising from the source and completes when the source completes.</summary>
        ///
        /// <remarks>For each observer, the registered intermediate observing object is not thread safe.
        /// That is, observations arising from the source must not be triggered concurrently
        /// on different threads.</remarks>
        ///
        /// <param name="collector">The function to update the state with each observation.</param>
        /// <param name="state">The initial state.</param>
        ///
        /// <returns>An Observable that triggers on the updated state values.</returns>
        member inline obs.scan(state:'U, collector:('U -> 'T -> 'U)) = Observable.scan collector state obs

        /// <summary>Creates an observer which permanently subscribes to the given observable and which calls
        /// the given function for each observation.</summary>
        ///
        /// <param name="callback">The function to be called on each observation.</param>
        member inline obs.add(callback:('T -> unit)) = Observable.add callback obs

        /// <summary>Creates an observer which subscribes to the given observable and which calls
        /// the given function for each observation.</summary>
        ///
        /// <param name="callback">The function to be called on each observation.</param>
        ///
        /// <returns>An object that will remove the callback if disposed.</returns>
        member inline obs.subscribe(callback:('T -> unit)) = Observable.subscribe callback obs

        /// <summary>Returns a new observable that triggers on the second and subsequent triggerings of the input observable.
        /// The Nth triggering of the input observable passes the arguments from the N-1th and Nth triggering as
        /// a pair. The argument passed to the N-1th triggering is held in hidden internal state until the
        /// Nth triggering occurs.</summary>
        ///
        /// <remarks>For each observer, the registered intermediate observing object is not thread safe.
        /// That is, observations arising from the source must not be triggered concurrently
        /// on different threads.</remarks>
        ///
        /// <returns>An Observable that triggers on successive pairs of observations from the input Observable.</returns>
        member inline obs.pairwise() = Observable.pairwise obs

/// <summary>Fluent extension operations on events.</summary>
[<AutoOpen>]
module EventExtensions =

    type IEvent<'Del,'T when 'Del : delegate<'T,unit> and 'Del :> Delegate> with

        /// <summary>Fires the output event when either of the input events fire.</summary>
        ///
        /// <param name="evt2">The second input event.</param>
        ///
        /// <returns>An event that fires when either of the input events fire.</returns>
        member inline evt.merge(evt2:IEvent<'Del2,'T>) =  Event.merge evt evt2

        /// <summary>Returns a new event that passes values transformed by the given function.</summary>
        ///
        /// <param name="mapping">The function to transform event values.</param>
        ///
        /// <returns>An event that passes the transformed values.</returns>
        member inline evt.map(mapping:('T -> 'U)) = Event.map mapping evt

        /// <summary>Returns a new event that listens to the original event and triggers the resulting
        /// event only when the argument to the event passes the given function.</summary>
        ///
        /// <param name="predicate">The function to determine which triggers from the event to propagate.</param>
        ///
        /// <returns>An event that only passes values that pass the predicate.</returns>
        member inline evt.filter(predicate:('T -> bool)) = Event.filter predicate evt

        /// <summary>Returns a new event that listens to the original event and triggers the
        /// first resulting event if the application of the predicate to the event arguments
        /// returned true, and the second event if it returned false.</summary>
        ///
        /// <param name="predicate">The function to determine which output event to trigger.</param>
        ///
        /// <returns>A tuple of events.  The first is triggered when the predicate evaluates to true
        /// and the second when the predicate evaluates to false.</returns>
        member inline evt.partition(predicate:('T -> bool)) = Event.partition predicate evt

        /// <summary>Returns a new event that listens to the original event and triggers the
        /// first resulting event if the application of the function to the event arguments
        /// returned a Choice1Of2, and the second event if it returns a Choice2Of2.</summary>
        ///
        /// <param name="splitter">The function to transform event values into one of two types.</param>
        ///
        /// <returns>A tuple of events.  The first fires whenever <c>splitter</c> evaluates to Choice1of1 and
        /// the second fires whenever <c>splitter</c> evaluates to Choice2of2.</returns>
        member inline evt.split(splitter:('T -> Choice<'U1,'U2>)) = Event.split splitter evt

        /// <summary>Returns a new event which fires on a selection of messages from the original event.
        /// The selection function takes an original message to an optional new message.</summary>
        ///
        /// <param name="chooser">The function to select and transform event values to pass on.</param>
        ///
        /// <returns>An event that fires only when the chooser returns Some.</returns>
        member inline evt.choose(chooser:('T -> 'U option)) = Event.choose chooser evt

        /// <summary>Returns a new event consisting of the results of applying the given accumulating function
        /// to successive values triggered on the input event.  An item of internal state
        /// records the current value of the state parameter.  The internal state is not locked during the
        /// execution of the accumulation function, so care should be taken that the
        /// input IEvent not triggered by multiple threads simultaneously.</summary>
        ///
        /// <param name="collector">The function to update the state with each event value.</param>
        /// <param name="state">The initial state.</param>
        ///
        /// <returns>An event that fires on the updated state values.</returns>
        member inline evt.scan(state:'U, collector:('U -> 'T -> 'U)) = Event.scan collector state evt

        /// <summary>Runs the given function each time the given event is triggered.</summary>
        ///
        /// <param name="callback">The function to call when the event is triggered.</param>
        member inline evt.add(callback:('T -> unit)) = Event.add callback evt

        /// <summary>Returns a new event that triggers on the second and subsequent triggerings of the input event.
        /// The Nth triggering of the input event passes the arguments from the N-1th and Nth triggering as
        /// a pair. The argument passed to the N-1th triggering is held in hidden internal state until the
        /// Nth triggering occurs.</summary>
        ///
        /// <returns>An event that triggers on pairs of consecutive values passed from the source event.</returns>
        member inline evt.pairwise() = Event.pairwise evt

/// <summary>Fluent extension operations on native pointers.</summary>
[<AutoOpen>]
module NativePtrExtensions =
    type nativeptr<'T when 'T : unmanaged> with

        [<Unverifiable>]
        [<NoDynamicInvocation>]
        /// <summary>Returns a machine address for a given typed native pointer.</summary>
        ///
        /// <returns>The machine address.</returns>
        member inline ptr.toNativeInt() = NativePtr.toNativeInt ptr

        [<Unverifiable>]
        [<NoDynamicInvocation>]
        /// <summary>Returns a typed native pointer by adding index * sizeof&lt;'T&gt; to the
        /// given input pointer.</summary>
        ///
        /// <param name="index">The index by which to offset the pointer.</param>
        ///
        /// <returns>A typed pointer.</returns>
        member inline ptr.add(index:int) = NativePtr.add ptr index

        [<Unverifiable>]
        [<NoDynamicInvocation>]
        /// <summary>Dereferences the given typed native pointer.</summary>
        ///
        /// <returns>The value at the pointer address.</returns>
        member inline ptr.read() = NativePtr.read ptr

        [<Unverifiable>]
        [<NoDynamicInvocation>]
        /// <summary>Assigns the <c>value</c> into the memory location referenced by the given typed native pointer.</summary>
        ///
        /// <param name="value">The value to assign.</param>
        member inline ptr.write(value) = NativePtr.write ptr value

        [<Unverifiable>]
        [<NoDynamicInvocation>]
        /// <summary>Gets or assigns the memory location referenced by the typed native
        /// pointer computed by adding index * sizeof&lt;'T&gt; to the given input pointer.</summary>
        member inline ptr.Item
            with get (index:int) = NativePtr.get ptr index
            and set index value = NativePtr.set ptr index value

        [<Unverifiable>]
        [<NoDynamicInvocation>]
        /// <summary>Converts a given typed native pointer to a managed pointer.</summary>
        ///
        /// <returns>The managed pointer.</returns>
        member inline ptr.toByRef() = NativePtr.toByRef ptr
