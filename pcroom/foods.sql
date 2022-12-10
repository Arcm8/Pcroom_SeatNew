drop table foods
/
create table foods(id number primary key, foodname varchar(20),foodcategory varchar(10) ,foodvalue varchar(20), img varchar(100), quntity varchar(20))
/
INSERT INTO foods (id , foodname , foodcategory ,foodvalue , img , quntity) VALUES ( 1,'짜장면','A','2500','@"C:\pcroom\jja.jpg"','100')
/
INSERT INTO foods (id , foodname , foodcategory ,foodvalue , img , quntity) VALUES  ( 2,'신라면','A','2000','@"C\pcroom\sin.jpg"','100')
/
INSERT INTO foods (id , foodname , foodcategory ,foodvalue , img , quntity) VALUES  (3,'김밥','B','3000','@"C:\pcroom\kimbab.png"','100' )
/
INSERT INTO foods (id , foodname , foodcategory ,foodvalue , img , quntity) VALUES  (4,'떡볶이','B','3000','@"C:\pcroom\dduck.png"','100' )
/