"""
Module with database model classes.
"""

from sqlalchemy import String
from sqlalchemy.orm import DeclarativeBase
from sqlalchemy.orm import Mapped
from sqlalchemy.orm import mapped_column


class Entity(DeclarativeBase):
    id: Mapped[int] = mapped_column(primary_key=True)


class JobResult(Entity):
    __tablename__ = "job_results"

    job_name: Mapped[str] = mapped_column(String(30))

    def __repr__(self) -> str:
        return f"JobResult(id={self.id!r}, job_name={self.job_name!r})"
