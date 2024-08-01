create table if not exists carepet.sensor
(
    pet_id uuid,
    sensor_id uuid,
    type text,
    primary key (pet_id, sensor_id)
);
