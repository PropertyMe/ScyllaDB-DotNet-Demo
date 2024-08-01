create table if not exists carepet.owner
(
    owner_id uuid,
    address text,
    name    text,
    primary key (owner_id)
);