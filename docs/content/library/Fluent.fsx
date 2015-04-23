(**

# Usage examples for all functions


The fluent-style members for all Seq.* operations are shown below. Some additional fluent functions are
available for Array, these are not shown here.

*)

#r @"../../../bin/3.1/FSharp.Core.Fluent-3.1.dll"
#r "System.Runtime" // in scripts you must also reference this DLL because you are using a portable component

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

#if FSHARP_CORE_AT_LEAST_4_4_0_0
seq1.contains(3) 
seq1.exceptseq2) 
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
#endif

