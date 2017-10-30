
#r "System.Transactions.dll" 
#r "../packages/Accord.3.8.0/lib/net40/Accord.dll" 
#r "../packages/Accord.Statistics.3.8.0/lib/net40/Accord.Statistics.dll" 
#r "../packages/Accord.Math.3.8.0/lib/net40/Accord.Math.dll" 
#r "../packages/FSharp.Data.2.4.2/lib/net45/FSharp.Data.dll"

open Accord
open Accord.Statistics
open Accord.Statistics.Models.Regression.Linear

open System
open System.Data.SqlClient

type ProductReview = {ProductID:int; TotalOrders:float; AvgReviews:float}
let reviews = ResizeArray<ProductReview>()
[<Literal>]
let connectionString = "data source=nc54a9m5kk.database.windows.net;initial catalog=AdventureWorks2014;user id=chickenskills@nc54a9m5kk;password=sk1lzm@tter;"
[<Literal>] 
let query = "Select	A.ProductID, TotalOrders, AvgReviews From																
                            (Select	ProductID, Sum(OrderQty) as TotalOrders	from [Sales].[SalesOrderDetail]	as	SOD inner	join
							[Sales].[SalesOrderHeader]	as	SOH	on	SOD.SalesOrderID	=	SOH.SalesOrderID	inner	join	
                            [Sales].[Customer]	as	C on	SOH.CustomerID	=	C.CustomerID Where	C.StoreID	is	not	null Group	By	ProductID)	as	A	Inner	Join 
                            (Select	ProductID, (Sum(Rating)	+	0.0)	/	(Count(ProductID)	+	0.0)	as	AvgReviews	from	[Production].[ProductReview]	as	PR	Group	By	ProductID)	as	B	
                            on	A.ProductID	=	B.ProductID" 

let connection = new SqlConnection(connectionString)
let command = new SqlCommand(query, connection)
connection.Open
printf "%A" connection.State
let reader = command.ExecuteReader()
while reader.Read() do 
    reviews.Add({ProductID = reader.GetInt32(0); 
    TotalOrders = (float) (reader.GetInt32(1)); 
    AvgReviews = (float) (reader.GetInt32(2))})
