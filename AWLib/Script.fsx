// Learn more about F# at http://fsharp.org. See the 'F# Tutorial' project
// for more guidance on F# programming.

#load "Library1.fs"
open AWLib

let input = [| 1,1.;2,2.;3,2.25;4,4.75;5,5.;|]
let variance (source:float seq) = 
    let mean = Seq.average source
    let deltas = Seq.map(fun x -> pown(x-mean) 2) source
    Seq.average deltas

let standardeviation values = 
    sqrt(variance values)

let x = input |> Seq.map(fun(x,y) -> float x)
let y = input |> Seq.map(fun(x,y) -> y)

let meanX = Seq.average x
let meanY = Seq.average y
let sdX = standardeviation x
let sdY = standardeviation y

let pearsoncorelation (a:float seq, b:float seq) = 
    let mX = Seq.average a
    let mY = Seq.average b
    let x = a |> Seq.map(fun i -> i - mX)
    let y = b |> Seq.map(fun i -> i - mY)
    let xys = Seq.zip x y
    let xy = xys |> Seq.map(fun(x,y) -> x*x, y*y, x*y)
    let sxy = xy |> Seq.sumBy (fun(xx, yy, xy) -> xy)
    let sx2 = xy |> Seq.sumBy (fun(xx, yy, xy) -> xx)
    let sy2 = xy |> Seq.sumBy (fun(xx, yy, xy) -> yy)
    sxy / sqrt (sx2*sy2)

let r = pearsoncorelation(x,y)

let b = r * (sdY/sdX)
let A = meanY - b*meanX
let Y' = input |> Seq.map(fun (i,j) -> j+A) 
printf "%A" (Y' |> Seq.toList)


    




