select * from AspNetUsers
where UserName='syovanna@hotmail.com';
select * from AspNetUsers
where PhoneNumber is not null;
select * from AspNetUserRoles ur , AspNetUsers u, AspNetRoles r
where UserName='syovanna@hotmail.com'
and ur.UserId=u.Id
and r.Id=ur.RoleId;

update alerts
set Status = 0