-- create a new table
create table if not exists
local_dev.contact (
     id UUID PRIMARY KEY,
     username TEXT,
     email TEXT,
     address TEXT
);
