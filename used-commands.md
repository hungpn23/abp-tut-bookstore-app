## Docker

docker run --name acmebookstore -p 5432:5432 -e POSTGRES_USER=hungpn23 -e POSTGRES_PASSWORD=hung1235 -e POSTGRES_DB=acmebookstore -d postgres

## Create solution

abp new Acme.BookStore -u blazor -dbms PostgreSQL -csf -cs "Server=localhost;Port=5432;Database=acmebookstore;User ID=hungpn23;Password=hung1235;"