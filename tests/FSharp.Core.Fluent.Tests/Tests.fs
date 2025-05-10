module Tests


open Expecto
open FSharp.Core.Fluent
open System.Collections.Generic

[<Tests>]
let tests =
    testList "Smoke tests" [
        testCase "Array smoke tests" <| fun _ ->
            let arr1 = [| 10 .. 20 |]
            let arr1a = [| 10 |]

            let _ = arr1.pick(fun n -> if n % 2 = 0 then Some n else None)
            let _ = arr1.pick(fun n -> if n % 2 = 0 then Some n else None)
            let _ : int[] = arr1.map (fun n -> n.CompareTo(3) + 1)

            let arr2,arr3 = arr1.zip(arr1).unzip()
            let arr2b,arr3b,arr4b = arr1.zip3(arr1, arr2).unzip3()

            let _ : int list = arr1.toList()
            let _ : seq<int> = arr1.toSeq()
            let _ : int option = arr1.tryFind(fun x -> x % 2 = 0)
            let _ : int = arr1.find(fun x -> x % 2 = 0)

            (arr1.allPairs(arr1) : array<int * int>) |> ignore
            (arr1.insertAt(1, 9) : array<int>) |> ignore
            (arr1.insertManyAt(1, [| 8; 9 |]) : array<int>) |> ignore
            (arr1.removeAt(1) : array<int>) |> ignore
            (arr1.removeManyAt(1, 2) : array<int>) |> ignore
            (arr1.splitAt(3) : array<int> * array<int>) |> ignore
            (arr1.tryExactlyOne() : option<int>) |> ignore
            (arr1.updateAt(1, 9) : array<int>) |> ignore
            (arr1.countBy(id) : array<int * int>) |> ignore
            (arr1.distinct() :  array<int>) |> ignore
            (arr1.distinctBy(id) :  array<int>) |> ignore
            (arr1.groupBy(id) : array<int * array<int>> ) |> ignore
            (arr1.pairwise() : array<int*int>) |> ignore
            (arr1.skip(3) : array<int>  ) |> ignore
            (arr1.skipWhile(fun x -> x > 1) :  array<int> ) |> ignore
            (arr1.take(3) : array<int>  ) |> ignore
            (arr1.takeWhile(fun x -> x > 1) : array<int> ) |> ignore
            (arr1.truncate(3) : array<int>  ) |> ignore
            (arr1.where(fun x -> x > 1) :  array<int> ) |> ignore
            (arr1.windowed(3) : array<int[]> ) |> ignore
            (arr1.contains(3) : bool  ) |> ignore
            (arr1.indexed() : (int * int)[]  ) |> ignore
            (arr1.sortDescending() : int[]  ) |> ignore
            (arr1.sortByDescending(id) : int[]  ) |> ignore
            (arr1.tryFindIndexBack(fun x -> x > 1) : int option ) |> ignore
            (arr1.tryFindBack(fun x -> x > 1) : int option  ) |> ignore
            (arr1.tryItem(19) : int option  ) |> ignore
            (arr1.tryHead() : int option  ) |> ignore
            (arr1.tryLast() : int option  ) |> ignore
            (arr1.tail() : int[]  ) |> ignore
            (* (arr1.except(seq1) : array<int>  ) |> ignore
            (arr1.chunkBySize(3) : array<int[]>  ) |> ignore
            (arr1.splitInto(3) : array<int[]>  ) |> ignore *)
            (arr1.append(arr1) : int[]) |> ignore
            (arr1.averageBy(float) : float) |> ignore
            (arr1.cache() : seq<int>) |> ignore
            (arr1.choose(fun x -> if x % 2 = 0 then Some x else None) : int[]) |> ignore
            (arr1.collect (fun n -> [| n; n + 1 |]) : int[]) |> ignore
            (arr1.copy() :  int[]) |> ignore
            (arr1.exists(fun x -> x > 1) :  bool) |> ignore
            (arr1.filter(fun x -> x > 1) : int[] ) |> ignore
            (arr1.find(fun x -> x > 1) : int) |> ignore
            (arr1.findIndex(fun x -> x > 1) : int  ) |> ignore
            (arr1.tryFind(fun x -> x > 1) : int option) |> ignore
            (arr1.tryFindIndex(fun x -> x > 1) : int option) |> ignore
            (arr1.tryPick(fun x -> Some x) : int option ) |> ignore
            (arr1.fold(3, fun z x -> x + z) :  int ) |> ignore
            (arr1.foldBack((fun x z -> x + z), 3) : int  ) |> ignore
            (arr1.forall(fun x -> x > 1) :  bool) |> ignore
            (arr1.iter(fun x -> sprintf "%d" x |> ignore) : unit ) |> ignore
            (arr1.iteri(fun i x -> sprintf "%d" x |> ignore) :  unit) |> ignore
            (arr1.map(fun x -> x + 1) : int[]  ) |> ignore
            (arr1.length : int  ) |> ignore
            (arr1.mapi(fun i x -> x + 1) : int[]   ) |> ignore
            (arr1.max() : int ) |> ignore
            (arr1.maxBy(id) : int  ) |> ignore
            (arr1.min() :  int) |> ignore
            (arr1.minBy(id) :  int ) |> ignore
            (arr1.partition(fun x -> x > 1) : int[] *int[]  ) |> ignore
            (arr1.permute(id) : int[]  ) |> ignore
            (arr1.pick(fun x -> Some x) : int) |> ignore
            (arr1.readonly() : seq<int>  ) |> ignore
            (arr1.reduce(+) : int ) |> ignore
            (arr1.reduceBack(+) : int  ) |> ignore
            (arr1.reverse() : int[]  ) |> ignore
            (arr1.scan(3, fun z x -> x + z) : int[] ) |> ignore
            (arr1.scanBack((+),3) : int[] ) |> ignore
            (arr1.sort() : int[]  ) |> ignore
            (arr1.sortBy(id) : int[]  ) |> ignore
            (arr1.sortInPlace() : unit  ) |> ignore
            (arr1.sortInPlaceBy(id) : unit  ) |> ignore
            (arr1.sortInPlaceWith(compare) : unit  ) |> ignore
            (arr1.sortWith(compare) : int[]  ) |> ignore
            (arr1.sum() : int  ) |> ignore
            (arr1.sumBy(id) : int  ) |> ignore
            (arr1.toArray() : int[]  ) |> ignore
            (arr1.toList() : int list ) |> ignore
            (arr1.toSeq() : seq<int>  ) |> ignore
            (arr1.tryFind(fun x -> x > 1) : int option  ) |> ignore
            (arr1.tryFindIndex(fun x -> x > 1) : int option ) |> ignore
            (arr1.tryPick(fun x -> Some x) : int option  ) |> ignore
            (arr1.zip(arr1) : (int * int) []  ) |> ignore
            (arr1.zip3(arr1,arr2) : (int * int * int)[]  ) |> ignore
            (arr1a.exactlyOne() :  int) |> ignore
            (arr1.permute(id) : int[]  ) |> ignore
            (arr1.reverse() : int[]  ) |> ignore
            (arr1.scanBack((+),3) : int[] ) |> ignore
            (arr1.sortWith(compare) : int[]  ) |> ignore
            (arr1.head() : int ) |> ignore
            (arr1.last() :  int) |> ignore

        testCase "List smoke tests" <| fun _ ->
            let list1 = [ 10 .. 20 ]
            let list1a = [ 10 ]

            list1
               .map(fun x -> x + 1)
               .filter(fun x -> x > 4) |> ignore

            list1
               |> List.map(fun x -> x + 1)
               |> List.filter(fun x -> x > 4) |> ignore

            list1
               .map(fun x -> x + 1)
               |> List.filter(fun x -> x > 4) |> ignore

            list1
               .map(fun x -> x + 1)
               .filter(fun x -> x > 4)
               .toArray |> ignore

            (list1.allPairs(list1) : list<int * int>) |> ignore
            (list1.insertAt(1, 9) : list<int>) |> ignore
            (list1.insertManyAt(1, [ 8; 9 ]) : list<int>) |> ignore
            (list1.removeAt(1) : list<int>) |> ignore
            (list1.removeManyAt(1, 2) : list<int>) |> ignore
            (list1.splitAt(3) : list<int> * list<int>) |> ignore
            (list1.tryExactlyOne() : option<int>) |> ignore
            (list1.updateAt(1, 9) : list<int>) |> ignore
            (list1.countBy(id) : list<int * int>) |> ignore
            (list1.distinct() :  list<int>) |> ignore
            (list1.distinctBy(id) :  list<int>) |> ignore
            (list1.groupBy(id) : list<int * list<int>> ) |> ignore
            (list1.pairwise() : list<int*int>) |> ignore
            (list1.skip(3) : list<int>  ) |> ignore
            (list1.skipWhile(fun x -> x > 1) :  list<int> ) |> ignore
            (list1.take(3) : list<int>  ) |> ignore
            (list1.takeWhile(fun x -> x > 1) : list<int> ) |> ignore
            (list1.truncate(3) : list<int>  ) |> ignore
            (list1.where(fun x -> x > 1) :  list<int> ) |> ignore
            // (list1.windowed(3) : list<int []> ) |> ignore
            (list1.indexed() : list<int * int>  ) |> ignore
            (* (list1.except(seq1) : list<int>  ) |> ignore
            (list1.chunkBySize(3) : list<int[]>  ) |> ignore
            (list1.splitInto(3) : list<int[]>  ) |> ignore *)

            let _ = list1.pick(fun n -> if n % 2 = 0 then Some n else None)
            let _ = list1.pick(fun n -> if n % 2 = 0 then Some n else None)
            let _ : int list = list1.map (fun n -> n.CompareTo(3) + 1) // use CompareTo to check members
            let _ : int list = list1.collect (fun n -> [ n; n + 1 ])

            let list2,list3 = list1.zip(list1).unzip()
            let list2b,list3b,list4b = list1.zip3(list1, list2).unzip3()

            let _ : int [] = list1.toArray()
            let _ : seq<int> = list1.toSeq()
            let _ : int option = list1.tryFind(fun x -> x % 2 = 0)
            let _ : int = list1.find(fun x -> x % 2 = 0)
            let _ : int  list = list1.choose(fun x -> if x % 2 = 0 then Some x else None)
            let _ : int = list1.reduce(+)
            let _ : int = list1.reduce(fun x y -> x * y % 29)
            let _ : int = list1.fold(3, fun z x -> x + z)
            let _ : int = list1.sum()
            let _ : int = list1.sumBy(fun x -> x + 1)

            (list1.append(list1) : int list) |> ignore
            (list1.averageBy(float) : float) |> ignore
            (list1.cache() : seq<int>) |> ignore
            (list1.choose(fun x -> if x % 2 = 0 then Some x else None) : int list) |> ignore
            (list1.collect (fun n -> [ n; n + 1 ]) : int list) |> ignore
            (list1a.exactlyOne() :  int) |> ignore
            (list1.exists(fun x -> x > 1) :  bool) |> ignore
            (list1.filter(fun x -> x > 1) : int list ) |> ignore
            (list1.find(fun x -> x > 1) : int) |> ignore
            (list1.findIndex(fun x -> x > 1) : int  ) |> ignore
            (list1.tryFind(fun x -> x > 1) : int option) |> ignore
            (list1.tryFindIndex(fun x -> x > 1) : int option) |> ignore
            (list1.tryPick(fun x -> Some x) : int option ) |> ignore
            (list1.fold(3, fun z x -> x + z) :  int ) |> ignore
            (list1.foldBack((fun x z -> x + z), 3) : int  ) |> ignore
            (list1.forall(fun x -> x > 1) :  bool) |> ignore
            (list1.head() : int ) |> ignore
            (list1.iter(fun x -> sprintf "%d" x |> ignore) : unit ) |> ignore
            (list1.iteri(fun i x -> sprintf "%d" x |> ignore) :  unit) |> ignore
            (list1.last() :  int) |> ignore
            (list1.length : int  ) |> ignore
            (list1.map(fun x -> x + 1) : int list  ) |> ignore
            (list1.mapi(fun i x -> x + 1) : int list   ) |> ignore
            (list1.max() : int ) |> ignore
            (list1.maxBy(id) : int  ) |> ignore
            (list1.min() :  int) |> ignore
            (list1.minBy(id) :  int ) |> ignore
            (list1.partition(fun x -> x > 1) : int list *int list  ) |> ignore
            (list1.permute(id) : int list  ) |> ignore
            (list1.pick(fun x -> Some x) : int) |> ignore
            (list1.reduce(+) : int ) |> ignore
            (list1.reduceBack(+) : int  ) |> ignore
            (list1.reverse() : int list  ) |> ignore
            (list1.scan(3, fun z x -> x + z) : int list ) |> ignore
            (list1.scanBack((+),3) : int list ) |> ignore
            (list1.readonly() : seq<int>  ) |> ignore
            (list1.sort() : int list  ) |> ignore
            (list1.sortBy(id) : int list  ) |> ignore
            (list1.sortWith(compare) : int list  ) |> ignore
            (list1.sum() : int  ) |> ignore
            (list1.sumBy(id) : int  ) |> ignore
            (list1.toArray() : int[]) |> ignore
            (list1.toList() : int list ) |> ignore
            (list1.toSeq() : seq<int>  ) |> ignore
            (list1.tryFind(fun x -> x > 1) : int option  ) |> ignore
            (list1.tryFindIndex(fun x -> x > 1) : int option ) |> ignore
            (list1.tryPick(fun x -> Some x) : int option  ) |> ignore
            (list1.zip(list1) : (int * int)  list  ) |> ignore
            (list1.zip3(list1,list2) : (int * int * int) list  ) |> ignore
            (* (list1.except(seq1) : list<int>  ) |> ignore
            (list1.chunkBySize(3) : list<int[]>  ) |> ignore
            (list1.splitInto(3) : list<int[]>  ) |> ignore *)

        testCase "Seq smoke tests" <| fun _ ->
            let seq1 = seq { 10 .. 20 }
            let seq1a = seq { 10 .. 10 }
            let _ : seq<int> = seq1.distinctBy(id)
            let _ = seq1.pick(fun n -> if n % 2 = 0 then Some n else None)
            let _ = seq1.pick(fun n -> if n % 2 = 0 then Some n else None)
            let _ : seq<int> = seq1.map (fun n -> n.CompareTo(3) + 1) // use CompareTo to check members
            let _ : seq<int> = seq1.collect (fun n -> [ n; n + 1 ])

            let seq2 : seq<int * int> = seq1.zip(seq1)
            let seq2b : seq<int * int * (int * int)> = seq1.zip3(seq1, seq2)

            let _ : int [] = seq1.toArray()
            let _ : int list = seq1.toList()
            let _ : int option = seq1.tryFind(fun x -> x % 2 = 0)
            let _ : int = seq1.find(fun x -> x % 2 = 0)
            let _ : seq<int> = seq1.choose(fun x -> if x % 2 = 0 then Some x else None)

            let _ : int = seq1.reduce(+)
            let _ : int = seq1.reduce(fun x y -> x * y % 29)
            let _ : int = seq1.fold(3, fun z x -> x + z)

            let _ : int = seq1.sum()
            let _ : int = seq1.sumBy(fun x -> x + 1)

            (seq1.allPairs(seq1) : seq<int * int>) |> ignore
            (seq1.insertAt(1, 9) : seq<int>) |> ignore
            (seq1.insertManyAt(1, seq { 8; 9 }) : seq<int>) |> ignore
            (seq1.removeAt(1) : seq<int>) |> ignore
            (seq1.removeManyAt(1, 2) : seq<int>) |> ignore
            (seq1.tryExactlyOne() : option<int>) |> ignore
            (seq1.updateAt(1, 9) : seq<int>) |> ignore
            (seq1.append(seq1) : seq<int>) |> ignore
            (seq1.averageBy(float) : float) |> ignore
            (seq1.cache() : seq<int>) |> ignore
            (seq1.choose(fun x -> if x % 2 = 0 then Some x else None) : seq<int>) |> ignore
            (seq1.collect (fun n -> [ n; n + 1 ]) : seq<int>) |> ignore
            (seq1.countBy(id) : seq<int * int>) |> ignore
            (seq1.distinct() :  seq<int>) |> ignore
            (seq1.distinctBy(id) :  seq<int>) |> ignore
            (seq1a.exactlyOne() :  int) |> ignore
            (seq1.exists(fun x -> x > 1) :  bool) |> ignore
            (seq1.filter(fun x -> x > 1) : seq<int> ) |> ignore
            (seq1.find(fun x -> x > 1) : int) |> ignore
            (seq1.findIndex(fun x -> x > 1) : int  ) |> ignore
            (seq1.tryFind(fun x -> x > 1) : int option) |> ignore
            (seq1.tryFindIndex(fun x -> x > 1) : int option) |> ignore
            (seq1.tryPick(fun x -> Some x) : int option ) |> ignore
            (seq1.fold(3, fun z x -> x + z) :  int ) |> ignore
            (seq1.forall(fun x -> x > 1) :  bool) |> ignore
            (seq1.groupBy(id) : seq<int * seq<int>> ) |> ignore
            (seq1.head() : int ) |> ignore
            (seq1.iter(fun x -> sprintf "%d" x |> ignore) : unit ) |> ignore
            (seq1.iteri(fun i x -> sprintf "%d" x |> ignore) :  unit) |> ignore
            (seq1.last() :  int) |> ignore
            (seq1.length : int  ) |> ignore
            (seq1.map(fun x -> x + 1) : seq<int>  ) |> ignore
            (seq1.mapi(fun i x -> x + 1) : seq<int>   ) |> ignore
            (seq1.max() : int ) |> ignore
            (seq1.maxBy(id) : int  ) |> ignore
            (seq1.min() :  int) |> ignore
            (seq1.minBy(id) :  int ) |> ignore
            (seq1.pairwise() : seq<int*int>) |> ignore
            //(seq1.partition(fun x -> x > 1) : seq<int> *seq<int>  ) |> ignore
            (seq1.pick(fun x -> Some x) : int) |> ignore
            (seq1.readonly() : seq<int>  ) |> ignore
            (seq1.reduce(+) : int ) |> ignore
            (seq1.scan(3, fun z x -> x + z) : seq<int> ) |> ignore
            (seq1.skip(3) : seq<int>  ) |> ignore
            (seq1.skipWhile(fun x -> x > 1) :  seq<int> ) |> ignore
            (seq1.sort() : seq<int>  ) |> ignore
            (seq1.sortBy(id) : seq<int>  ) |> ignore
            (seq1.sum() : int  ) |> ignore
            (seq1.sumBy(id) : int  ) |> ignore
            (seq1.take(3) : seq<int>  ) |> ignore
            (seq1.takeWhile(fun x -> x > 1) : seq<int> ) |> ignore
            (seq1.toArray() : int[]) |> ignore
            (seq1.toList() : list<int> ) |> ignore
            (seq1.toArray() : int[]  ) |> ignore
            (seq1.truncate(3) : seq<int>  ) |> ignore
            (seq1.tryFind(fun x -> x > 1) : int option  ) |> ignore
            (seq1.tryFindIndex(fun x -> x > 1) : int option ) |> ignore
            (seq1.tryPick(fun x -> Some x) : int option  ) |> ignore
            (seq1.where(fun x -> x > 1) :  seq<int> ) |> ignore
            (seq1.windowed(3) : seq<int []> ) |> ignore
            (seq1.zip(seq1) : seq<int * int> ) |> ignore
            (seq1.zip3(seq1,seq2) : seq<int * int * (int * int)>) |> ignore
            (seq1.mapFold(1, fun z x -> x + 1, z) : seq<int> * int  ) |> ignore
            (seq1.mapFoldBack((fun z x -> x + 1, z), 1) : seq<int> * int  ) |> ignore
            (seq1.reduceBack(+) : int  ) |> ignore
            (seq1.foldBack((fun x z -> x + z), 3) : int  ) |> ignore
            (seq1.contains(3) : bool  ) |> ignore
            (seq1.permute(id) : seq<int>  ) |> ignore
            (seq1.reverse() : seq<int>  ) |> ignore
            (seq1.indexed() : seq<int * int>  ) |> ignore
            (seq1.scanBack((+),3) : seq<int> ) |> ignore
            (seq1.sortWith(compare) : seq<int>  ) |> ignore
            (seq1.sortDescending() : seq<int>  ) |> ignore
            (seq1.sortByDescending(id) : seq<int>  ) |> ignore
            (seq1.tryFindIndexBack(fun x -> x > 1) : int option ) |> ignore
            (seq1.tryFindBack(fun x -> x > 1) : int option  ) |> ignore
            (seq1.tryItem(19) : int option  ) |> ignore
            (seq1.tryHead() : int option  ) |> ignore
            (seq1.tryLast() : int option  ) |> ignore
            (seq1.tail() : seq<int>  ) |> ignore
            (seq1.except(seq1) : seq<int>  ) |> ignore
            (seq1.chunkBySize(3) : seq<int[]>  ) |> ignore
            (seq1.splitInto(3) : seq<int[]>  ) |> ignore

        testCase "Array2D smoke test" <| fun _ ->
            let arr1 = array2D [ [ 1..10]; [1..10] ]

            (arr1.base1 : int) |> ignore
            (arr1.base2 : int) |> ignore
            (arr1.length1 : int) |> ignore
            (arr1.copy() : int[,]) |> ignore
            (arr1.iter(fun i -> (i : int) |> ignore)) |> ignore
            (arr1.iteri(fun i j k -> (i : int) |> ignore;
                                     (j : int) |> ignore;
                                     (k : int) |> ignore)) |> ignore
            (arr1.map(fun k -> (k : int))) |> ignore
            (arr1.mapi(fun i j k -> (i : int) |> ignore;
                                    (j : int) |> ignore;
                                    (k : int))) |> ignore
            (arr1.rebase() : int[,]) |> ignore

        testCase "Array3D smoke test" <| fun _ ->
            let arr1 = Array3D.zeroCreate<int> 3 4 5

            (arr1.length1 : int) |> ignore
            (arr1.length2 : int) |> ignore
            (arr1.length3 : int) |> ignore
            (arr1.iter(fun i -> (i : int) |> ignore)) |> ignore
            (arr1.iteri(fun i1 i2 i3 k ->
                          (i1 : int) |> ignore;
                          (i2 : int) |> ignore;
                          (i3 : int) |> ignore;
                          (k : int) |> ignore)) |> ignore
            (arr1.map(fun k -> (k : int))) |> ignore
            (arr1.mapi(fun i1 i2 i3 k ->
                         (i1 : int) |> ignore;
                         (i2 : int) |> ignore;
                         (i3 : int) |> ignore;
                         k)) |> ignore

        testCase "Array4D smoke test" <| fun _ ->
            let arr1 = Array4D.zeroCreate<int> 3 4 5 6

            (arr1.length1 : int) |> ignore
            (arr1.length2 : int) |> ignore
            (arr1.length3 : int) |> ignore
            (arr1.length4 : int) |> ignore

        testCase "Option smoke test" <| fun _ ->
            let opt0 = (None: int option)
            let opt1 = Some 3

            let _ : int = opt0.count
            let _ : int option = opt0.bind(fun x -> None)
            let _ : int option = opt0.filter(fun x -> true)
            let _ : bool = opt0.exists(fun x -> true)
            let _ : int = opt0.fold(1, fun z x -> 2)
            let _ : int = opt0.foldBack((fun x z -> z), 1)
            let _ : bool = opt0.forall(fun x -> true)
            let _ : unit = opt0.iter(fun x -> ())
            let _ : bool option = opt0.map(fun x -> true)
            let _ : int[] = opt0.toArray()
            let _ : int list = opt0.toList()
            let _ : System.Nullable<int> = opt0.toNullable()
            let _ : int = opt1.count

            ()

        testCase "String smoke test" <| fun _ ->
            let str0 = ""
            let str1 = "abc"

            let _ = str0.length
            let _ : bool = str0.exists(fun x -> true)
            let _ : int = str0.fold(1, fun z x -> 2)
            let _ : int = str0.foldBack((fun x z -> z), 1)
            let _ : bool = str0.forall(fun x -> true)
            let _ : unit = str0.iter(fun x -> ())
            let _ : string = str0.map(fun x -> 'c')
            let _ : char[] = str0.toArray()
            let _ : char list = str0.toList()

            ()

        testCase "Pipe test" <| fun _ ->
            (4.0).pipe(sqrt) |> ignore
            "abc".pipe(String.length) |> ignore
            let magnitude (vec:list<float>) = vec.sumBy(fun x -> x * x).pipe(sqrt)
            magnitude [1.0;2.0] |> ignore
            ()
  ]
