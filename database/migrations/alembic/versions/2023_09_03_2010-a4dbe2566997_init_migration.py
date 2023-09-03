"""init migration

Revision ID: a4dbe2566997
Revises: 
Create Date: 2023-09-03 20:10:09.379188

"""
from typing import Sequence, Union

from alembic import op
import sqlalchemy as sa


# revision identifiers, used by Alembic.
revision: str = 'a4dbe2566997'
down_revision: Union[str, None] = None
branch_labels: Union[str, Sequence[str], None] = None
depends_on: Union[str, Sequence[str], None] = None


def upgrade() -> None:
    op.create_table(
        "job_results",
        sa.Column("id", sa.Integer, primary_key=True),
        sa.Column("job_name", sa.String(120), nullable=False),
        sa.Column("run_date", sa.TIMESTAMP(), nullable=False),
    )


def downgrade() -> None:
    op.drop_table("job_results")
