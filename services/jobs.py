"""
Module with background tasks.
"""

from sqlalchemy import Connection


class HelloJob:
    """
    A class for a basic job that prints 'Hello from [job_name]!'.
    """
    def __init__(self) -> None:
        self._job_name = "hello_job"

    def job(self):
        print(f"Hello from {self._job_name}!")


class SqlInsertJob:
    """
    A class for a job that inserts a job result to the database.
    """
    def __init__(self):
        self._job_name = "sql_insert_job"

    def job(self, conn: Connection):
        print(f"Executing => {self._job_name}")
        print(conn.info)


class SqlReadJob:
    """
    A class for a job that reads the latest job result in the database.
    """
    def __init__(self):
        self._job_name = "sql_read_job"

    def job(self, conn: Connection):
        print(f"Executing => {self._job_name}")
        print(conn.info)
