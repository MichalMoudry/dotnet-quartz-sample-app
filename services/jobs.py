"""
Module with background tasks.
"""

from sqlalchemy import select, insert, Engine
from sqlalchemy.orm import Session
from database import model


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

    def job(self, engine: Engine):
        print(f"Executing => {self._job_name}")
        session = Session(engine)
        session.add(model.JobResult(self._job_name))
        session.commit()


class SqlReadJob:
    """
    A class for a job that reads the latest job result in the database.
    """
    def __init__(self):
        self._job_name = "sql_read_job"

    def job(self, engine: Engine):
        print(f"Executing => {self._job_name}")
        session = Session(engine)
        stmt = select(model.JobResult).where(model.JobResult.job_name.is_("sql_insert_job"))
        print(30 * "-")
        for result in session.scalars(stmt):
            print(result)
        print(30 * "-")
