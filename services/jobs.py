"""
Module with background tasks.
"""

from abc import ABC, abstractmethod

class BaseJob(ABC):
    """
    A base class for job classes.
    """
    def __init__(self, job_name: str) -> None:
        self._job_name = job_name
    
    @property
    @abstractmethod
    def job(self) -> (...):
        """
        A property containing code that will be executed.
        """
        pass


class HelloJob(BaseJob):
    """
    A class for a basic job that prints 'Hello!'.
    """
    def __init__(self, job_name: str) -> None:
        super().__init__(job_name)


def hello_job():
    """
    A simple hello job that prints 'Hello!'.
    """
    print("Hello!")


def sqlite_read_job():
    ...


def sqlite_insert_job():
    ...
