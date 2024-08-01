create table if not exists carepet.pet
(
    owner_id uuid,
    pet_id   uuid,
    chip_id  text,
    species  text,
    breed    text,
    color    text,
    gender   text,
    age      int,
    weight   float,
    address  text,
    name     text,
    primary key (owner_id, pet_id)
);
