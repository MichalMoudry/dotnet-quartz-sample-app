"""
Module with the sample app.
"""

import schedule
import time
from database import db_init
from services import jobs

if __name__ == "__main__":
    engine = db_init.get_engine(True)
    db_init.create_tables(engine)

    schedule.every(5).seconds.do(jobs.HelloJob().job)
    with engine.connect() as conn:
        schedule.every(30).seconds.do(jobs.SqlReadJob().job, engine)
        schedule.every(1).minute.do(jobs.SqlInsertJob().job, engine)
        while True:
            schedule.run_pending()
            time.sleep(1)
