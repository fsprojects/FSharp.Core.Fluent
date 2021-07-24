(*** hide ***)
#I "../src/FSharp.Core.Fluent/bin/Release/netstandard2.0"
#r "FSharp.Core.Fluent.dll"
(**

# Usage examples

## Seq functions

The fluent-style members for all Seq.* operations are shown below. Some additional fluent functions are
available for Array, these are not shown here.

*)
open FSharp.Core.Fluent

module SeqExamples =
    let seq1 = seq { 1 .. 10 }
    let seq2 = seq { 11 .. 20 }

    seq1.append(seq2)
    seq1.averageBy(fun x -> float x)
    seq1.cache()
    seq1.choose(fun x -> if x % 2 = 0 then Some x else None)
    seq1.collect (fun n -> [ n; n + 1 ])
    seq1.countBy(fun x -> x)
    seq1.distinct()
    seq1.distinctBy(fun x -> x)
    seq1.exactlyOne()
    seq1.exists(fun x -> x > 1)
    seq1.filter(fun x -> x > 1)
    seq1.find(fun x -> x > 1)
    seq1.findIndex(fun x -> x > 1)
    seq1.tryFind(fun x -> x > 1)
    seq1.tryFindIndex(fun x -> x > 1)
    seq1.tryPick(fun x -> Some x)
    seq1.fold(3, fun z x -> x + z)
    seq1.forall(fun x -> x > 1)
    seq1.groupBy(fun x -> x)
    seq1.head()
    seq1.iter(fun x -> printfn "%d" x)
    seq1.iteri(fun i x ->  printfn "%d" x)
    seq1.last()
    seq1.length
    seq1.map(fun x -> x + 1)
    seq1.mapi(fun i x -> x + 1)
    seq1.max()
    seq1.maxBy(fun x -> x)
    seq1.min()
    seq1.minBy(fun x -> x)
    seq1.pairwise()
    seq1.pick(fun x -> Some x)
    seq1.readonly()
    seq1.reduce(+)
    seq1.scan(3, fun z x -> x + z)
    seq1.skip(3)
    seq1.skipWhile(fun x -> x > 1)
    seq1.sort()
    seq1.sortBy(fun x -> x)
    seq1.sum()
    seq1.sumBy(fun x -> x)
    seq1.take(3)
    seq1.takeWhile(fun x -> x > 1)
    seq1.toArray()
    seq1.toList()
    seq1.toArray()
    seq1.truncate(3)
    seq1.tryFind(fun x -> x > 1)
    seq1.tryFindIndex(fun x -> x > 1)
    seq1.tryPick(fun x -> Some x)
    seq1.where(fun x -> x > 1)
    seq1.windowed(3)
    seq1.zip(seq2)
    seq1.zip3(seq1,seq2)
    seq1.contains(3)
    seq1.except(seq2)
    seq1.permute(fun x -> x)
    seq1.reduceBack(+)
    seq1.foldBack((fun x z -> x + z), 3)
    seq1.reverse()
    seq1.scanBack((+),3)
    seq1.sortWith(compare)
    seq1.sortDescending()
    seq1.chunkBySize(3)
    seq1.splitInto(3)
    seq1.tryFindIndexBack(fun x -> x > 1)
    seq1.tryFindBack(fun x -> x > 1)
    seq1.tryItem(19)
    seq1.tail()

(**
# Array functions

The fluent-style members for Array.* operations are shown below.

*)

module ArrayExamples =
    let array1 = [| 1 .. 10 |]
    let array2 = [| 11 .. 20 |]

    array1.append(array2)
    array1.averageBy(fun x -> float x)
    array1.cache()
    array1.choose(fun x -> if x % 2 = 0 then Some x else None)
    array1.collect (fun n -> [ n; n + 1 ])
    array1.countBy(fun x -> x)
    array1.distinct()
    array1.distinctBy(fun x -> x)
    array1.exactlyOne()
    array1.exists(fun x -> x > 1)
    array1.filter(fun x -> x > 1)
    array1.find(fun x -> x > 1)
    array1.findIndex(fun x -> x > 1)
    array1.tryFind(fun x -> x > 1)
    array1.tryFindIndex(fun x -> x > 1)
    array1.tryPick(fun x -> Some x)
    array1.fold(3, fun z x -> x + z)
    array1.forall(fun x -> x > 1)
    array1.groupBy(fun x -> x)
    array1.head()
    array1.iter(fun x -> printfn "%d" x)
    array1.iteri(fun i x ->  printfn "%d" x)
    array1.last()
    array1.length
    array1.map(fun x -> x + 1)
    array1.mapi(fun i x -> x + 1)
    array1.max()
    array1.maxBy(fun x -> x)
    array1.min()
    array1.minBy(fun x -> x)
    array1.pairwise()
    array1.pick(fun x -> Some x)
    array1.readonly()
    array1.reduce(+)
    array1.scan(3, fun z x -> x + z)
    array1.skip(3)
    array1.skipWhile(fun x -> x > 1)
    array1.sort()
    array1.sortBy(fun x -> x)
    array1.sum()
    array1.sumBy(fun x -> x)
    array1.take(3)
    array1.takeWhile(fun x -> x > 1)
    array1.toArray()
    array1.toList()
    array1.toArray()
    array1.truncate(3)
    array1.tryFind(fun x -> x > 1)
    array1.tryFindIndex(fun x -> x > 1)
    array1.tryPick(fun x -> Some x)
    array1.where(fun x -> x > 1)
    array1.windowed(3)
    array1.zip(array2)
    array1.zip3(array1,array2)
    array1.contains(3)
    array1.except(array2)
    array1.permute(fun x -> x)
    array1.reduceBack(+)
    array1.foldBack((fun x z -> x + z), 3)
    array1.reverse()
    array1.scanBack((+),3)
    array1.sortWith(compare)
    array1.sortDescending()
    array1.chunkBySize(3)
    array1.splitInto(3)
    array1.tryFindIndexBack(fun x -> x > 1)
    array1.tryFindBack(fun x -> x > 1)
    array1.tryItem(19)
    array1.tail()

(**
# List functions

The fluent-style members for Array.* operations are shown below.

*)

module ListExamples =
    let list1 = [ 1 .. 10 ]
    let list2 = [ 11 .. 20 ]

    list1.append(list2)
    list1.averageBy(fun x -> float x)
    list1.cache()
    list1.choose(fun x -> if x % 2 = 0 then Some x else None)
    list1.collect (fun n -> [ n; n + 1 ])
    list1.countBy(fun x -> x)
    list1.distinct()
    list1.distinctBy(fun x -> x)
    list1.exactlyOne()
    list1.exists(fun x -> x > 1)
    list1.filter(fun x -> x > 1)
    list1.find(fun x -> x > 1)
    list1.findIndex(fun x -> x > 1)
    list1.tryFind(fun x -> x > 1)
    list1.tryFindIndex(fun x -> x > 1)
    list1.tryPick(fun x -> Some x)
    list1.fold(3, fun z x -> x + z)
    list1.forall(fun x -> x > 1)
    list1.groupBy(fun x -> x)
    list1.head()
    list1.iter(fun x -> printfn "%d" x)
    list1.iteri(fun i x ->  printfn "%d" x)
    list1.last()
    list1.length
    list1.map(fun x -> x + 1)
    list1.mapi(fun i x -> x + 1)
    list1.max()
    list1.maxBy(fun x -> x)
    list1.min()
    list1.minBy(fun x -> x)
    list1.pairwise()
    list1.pick(fun x -> Some x)
    list1.readonly()
    list1.reduce(+)
    list1.scan(3, fun z x -> x + z)
    list1.skip(3)
    list1.skipWhile(fun x -> x > 1)
    list1.sort()
    list1.sortBy(fun x -> x)
    list1.sum()
    list1.sumBy(fun x -> x)
    list1.take(3)
    list1.takeWhile(fun x -> x > 1)
    list1.toArray()
    list1.toList()
    list1.toArray()
    list1.truncate(3)
    list1.tryFind(fun x -> x > 1)
    list1.tryFindIndex(fun x -> x > 1)
    list1.tryPick(fun x -> Some x)
    list1.where(fun x -> x > 1)
    list1.windowed(3)
    list1.zip(list2)
    list1.zip3(list1,list2)
    list1.contains(3)
    list1.except(list2)
    list1.permute(fun x -> x)
    list1.reduceBack(+)
    list1.foldBack((fun x z -> x + z), 3)
    list1.reverse()
    list1.scanBack((+),3)
    list1.sortWith(compare)
    list1.sortDescending()
    list1.chunkBySize(3)
    list1.splitInto(3)
    list1.tryFindIndexBack(fun x -> x > 1)
    list1.tryFindBack(fun x -> x > 1)
    list1.tryItem(19)
    list1.tail()

(**
# Option functions

The fluent-style members for Option.* operations are shown below.

*)

module OptionExamples =
    let option1 = Some 1
    let option2 = Some "two"

    option1.exists(fun x -> x > 1)
    option1.filter(fun x -> x > 1)
    option1.fold(3, fun z x -> x + z)
    option1.forall(fun x -> x > 1)
    option1.iter(fun x -> printfn "%d" x)
    option1.map(fun x -> x + 1)
    option1.toArray()
    option1.toList()
    option1.toArray()
    option1.foldBack((fun x z -> x + z), 3)
    option1.bind(fun x -> if x > 1 then None else Some (x+1))
    option1.toNullable()
    option2.toObj()

(**
# Observable functions

The fluent-style members for Observable.* operations are shown below.

*)

module ObservableExamples =
    open System
    let ev1 = Event<int>()
    let ev2 = Event<int>()
    let obs1 : IObservable<int> = ev1.Publish :> _
    let obs2 : IObservable<int> = ev2.Publish :> _

    obs1.filter(fun x -> x > 1)
    obs1.map(fun x -> x + 1)
    obs1.add(fun v -> printfn "%d" v)
    obs1.choose(fun v -> if v = 1 then Some 2 else None)
    obs1.merge(obs2)
    obs1.pairwise()
    obs1.partition(fun v -> v > 1)
    obs1.scan((fun a b -> a + b), 0)
    obs1.split(fun v -> if v > 1 then Choice1Of2 v else Choice2Of2 (-v))


(**
# Event functions

The fluent-style members for Event.* operations are shown below.

*)

module EventExamples =
    open System
    let e1 = Event<int>()
    let e2 = Event<int>()
    let ev1 = e1.Publish
    let ev2 = e2.Publish

    ev1.filter(fun x -> x > 1)
    ev1.map(fun x -> x + 1)
    ev1.add(fun v -> printfn "%d" v)
    ev1.choose(fun v -> if v = 1 then Some 2 else None)
    ev1.merge(ev2)
    ev1.pairwise()
    ev1.partition(fun v -> v > 1)
    ev1.scan(0, (fun a b -> a + b))
    ev1.split(fun v -> if v > 1 then Choice1Of2 v else Choice2Of2 (-v))

(**
# String functions

The fluent-style members for String.* operations are shown below.

*)

module StringExamples =
    open System
    let string1 = "a"

    string1.map(fun x -> 'a')
    string1.mapi(fun i x -> 'a')
    string1.collect(fun x -> "aa")
    string1.pairwise()
    string1.replicate(3)
    string1.length
