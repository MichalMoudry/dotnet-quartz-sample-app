"""
Module with database model classes.
"""
from datetime import datetime
from sqlalchemy import String, TIMESTAMP
from sqlalchemy.orm import DeclarativeBase
from sqlalchemy.orm import Mapped
from sqlalchemy.orm import mapped_column


class Entity(DeclarativeBase):
    id: Mapped[int] = mapped_column(primary_key=True)


class JobResult(Entity):
    __tablename__ = "job_results"

    job_name = mapped_column(String(30))
    run_date = mapped_column(TIMESTAMP())

    def __init__(self, job_name: str):
        self.job_name = job_name
        self.run_date = datetime.utcnow()

    def __repr__(self) -> str:
        return f"JobResult(id={self.id!r}, job_name={self.job_name!r}, run_date={self.run_date:%Y-%m-%d %H:%M:%S%z})"
