
let input = [| 1,1.;2,2.;3,2.25;4,4.75;5,5.;|]
let x = input |> Array.map(fun (i,j) -> float i)
let y = input |> Array.map(fun (i,j) -> j)
let mX = Array.average x
let mY = Array.average y

#r "../packages/MathNet.Numerics.3.20.0/lib/net40/MathNet.Numerics.dll"
open MathNet.Numerics.Statistics

let sdX = ArrayStatistics.StandardDeviation x
let sdY = ArrayStatistics.StandardDeviation y
let r = Correlation.Pearson (x,y)
let b = r * (sdY/sdX)
let A = mY - b*mX
//let Y = input |> Seq.map(fun (i,j) -> j+A) 
//printf "%A" (Y |> Seq.toList)

open MathNet.Numerics
let fit = Fit.Line(x,y)
let i = fst fit
let s = snd fit

#r "../packages/Accord.3.8.0/lib/net40/Accord.dll" 
#r "../packages/Accord.Statistics.3.8.0/lib/net40/Accord.Statistics.dll" 
#r "../packages/Accord.Math.3.8.0/lib/net40/Accord.Math.dll"
open Accord
open Accord.Statistics.Models.Regression.Linear 

let regression = SimpleLinearRegression()
let sse = regression.Regress(x,y)
let intercept = regression.Intercept
let slope = regression.Slope
let mse = sse / float x.Length
let rmse = sqrt mse
let r2 = regression.CoefficientOfDetermination(x,y)