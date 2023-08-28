/// <summary>
/// A module with all the SQL queries.
/// </summary>
module SampleAppQuartz.Database.Queries

/// A method that returns a SQL query that creates all the tables in the DB.
let CreateTablesQuery() =
    """
    CREATE TABLE IF NOT EXISTS
    JobResults (
        Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
        JobName TEXT,
        JobType INTEGER,
        StartDate TEXT,
        FinishDate TEXT
    );
    """