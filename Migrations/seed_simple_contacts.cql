-- note that we dont use a batch here, as that would increase the work for the coordinator (node)
-- if we batched this then we would force the nodes to write "all or nothing"
-- this in turn increases the perf cost as there's more networking to coordinate this atomicty between nodes AND partitions
--      in this case, contact has a UUID PK which creates a partition per contact

insert into local_dev.contact (id, username, email, address)
values (uuid(), 'dev user', 'dev@propertyme.com', '1 George Street, Sydney, NSW 2000')
if not exists;

insert into local_dev.contact (id, username, email, address)
values (uuid(), 'john curtain', 'johncurtain@propertyme.com', '1214 McDonald Avenue, Altamonte Springs, FL 32701')
if not exists;

insert into local_dev.contact (id, username, email, address)
values (uuid(), 'richard hammond', 'richardhammond@propertyme.com', '4270 Midway Road, Springdale, AR 72764')
if not exists;

insert into local_dev.contact (id, username, email, address)
values (uuid(), 'peter f. hamilton', 'peterfhamilton@propertyme.com', '3133 Conifer Drive, Washington, WA 98133')
if not exists;

insert into local_dev.contact (id, username, email, address)
values (uuid(), 'tracy harding', 'tracyharding@propertyme.com', '2077 Duke Lane, Weehawken, NJ 07087')
if not exists;

insert into local_dev.contact (id, username, email, address)
values (uuid(), 'lucy hawking', 'lucyhawking@propertyme.com', '185 Lauren Drive, Dodgeville, WI 52650')
if not exists;

insert into local_dev.contact (id, username, email, address)
values (uuid(), 'derek landy', 'dereklandy@propertyme.com', '1554 Southern Avenue, Mason City, IA 50401')
if not exists;

-- use double '' to escape a single ' char
insert into local_dev.contact (id, username, email, address)
values (uuid(), 'chris d''lacy', 'chrisdlacey@propertyme.com', '1819 Elliot Avenue, Seattle, WA 98106')
if not exists;

insert into local_dev.contact (id, username, email, address)
values (uuid(), 'andy kirk', 'andy kirk@propertyme.com', '1520 Rosebud Avenue, Tyronza, AR 72386')
if not exists;

insert into local_dev.contact (id, username, email, address)
values (uuid(), 'nate silver', 'natesilver@propertyme.com', '4691 Keyser Ridge Road, Greensboro, NC 27401')
if not exists;
