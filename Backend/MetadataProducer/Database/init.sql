CREATE DATABASE db;
USE db;

CREATE TABLE Category (
  ID CHAR(36) PRIMARY KEY,
  Name NVARCHAR(500) NOT NULL
);

ALTER user 'root' IDENTIFIED WITH mysql_native_password BY '';
flush privileges;