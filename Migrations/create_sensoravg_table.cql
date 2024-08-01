create table if not exists carepet.sensor_average
(
    sensor_id uuid,
    date  DATE,
    hour  int,
    value float,
    primary key (sensor_id, date, hour)
) with compaction = { 'class' : 'TimeWindowCompactionStrategy' };