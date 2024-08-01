create table if not exists carepet.measurement
(
    sensor_id uuid,
    ts    timestamp,
    value float,
    PRIMARY KEY (sensor_id, ts)
) with compaction = { 'class' : 'TimeWindowCompactionStrategy' };
