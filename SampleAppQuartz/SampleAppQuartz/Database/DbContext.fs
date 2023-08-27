module SampleAppQuartz.Database.DbContext

open System.Data

[<Sealed>]
type DbContext(dbConnection: IDbConnection) =
    do printfn ""
