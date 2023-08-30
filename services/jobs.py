"""
Module with background tasks.
"""

from abc import ABC
from sqlalchemy import select
from database import model, Session


class BaseJob(ABC):
    """
    A base class for all scheduled jobs.
    """
    ...


class HelloJob(BaseJob):
    """
    A class for a basic job that prints 'Hello from [job_name]!'.
    """
    def __init__(self) -> None:
        self._job_name = "hello_job"

    def job(self):
        print(f"Hello from {self._job_name}!")


class SqlInsertJob(BaseJob):
    """
    A class for a job that inserts a job result to the database.
    """
    def __init__(self):
        self._job_name = "sql_insert_job"

    def job(self):
        print(f"\nExecuting => {self._job_name}")
        session = Session()
        session.add(model.JobResult(self._job_name))
        session.commit()
        print(f"Ending => {self._job_name}")


class SqlReadJob(BaseJob):
    """
    A class for a job that reads the latest job result in the database.
    """
    def __init__(self):
        self._job_name = "sql_read_job"

    def job(self):
        print(f"\nExecuting => {self._job_name}")
        session = Session()
        stmt = select(model.JobResult).where(model.JobResult.job_name.is_("sql_insert_job"))
        print(30 * "-")
        for result in session.scalars(stmt):
            print(result)
        print(30 * "-")
        print(f"Ending => {self._job_name}")
