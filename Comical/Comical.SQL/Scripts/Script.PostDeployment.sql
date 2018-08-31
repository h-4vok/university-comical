/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

-- ROLES
INSERT Role (
	Code,
	Description,
	Enabled
)
SELECT
	Code = data.Code,
	Description = data.Description,
	Enabled = 1
FROM	(
			SELECT Code = 'admin', Description = 'Administrador' UNION
			SELECT Code = 'user', Description = 'Usuario' UNION
			SELECT Code = 'support', Description = 'Soporte'
		) data

LEFT  JOIN	Role R
		ON	r.Code = data.Code
WHERE		r.Id IS NULL

-- PERMISSIONS
INSERT Permission ( Code )
VALUES
	( 'User_New' ),
	( 'User_Update' ),
	( 'User_Disable' ),
	( 'User_Enable' ),
	( 'User_Delete' ),
	( 'User_Read' ),
	( 'ShoppingCart_CanUse' ),
	( 'Account_CanUse' ), 
	( 'InfoLogging_CanRead' ),
	( 'ErrorLogging_CanRead' )

-- USERS
INSERT [User] (
	Login,
	Password,
	Enabled,
	Blocked
)
SELECT
	Login = data.Login,
	Password = data.Password,
	Enabled = 1,
	Blocked = 1
FROM		(
				SELECT Login = 'admin', Password = 'admin' UNION
				SELECT Login = 'test', Password = 'test'
			) data

LEFT  JOIN	[User] U
		ON	data.Login = u.Login

WHERE		u.Id IS NULL

-- ROLES TO PERMISSIONS FOR admin ROLE
DELETE		RP
FROM		RolePermission RP
INNER JOIN	Role R
		ON	RP.RoleId = r.Id
WHERE		R.Code = 'admin'

INSERT		RolePermission (
	RoleId,
	PermissionId
)
SELECT
	RoleId = r.Id,
	PermimssionId = p.Id
FROM		Role R, Permission P
WHERE		R.Code = 'admin'

-- ROLES TO PERMISSIONS FOR user ROLE
INSERT		RolePermission (
	RoleId,
	PermissionId
)
SELECT
	RoleId = r.Id,
	PermimssionId = p.Id
FROM		Role R, Permission P
WHERE		R.Code = 'user'
AND			P.Code IN (
	'ShoppingCart_CanUse',
	'Account_CanUse'
)

-- ROLES TO PERMISSIONS FOR support ROLE
INSERT		RolePermission (
	RoleId,
	PermissionId
)
SELECT
	RoleId = r.Id,
	PermimssionId = p.Id
FROM		Role R, Permission P
WHERE		R.Code = 'support'
AND			P.Code IN (
	'InfoLogging_CanRead',
	'ErrorLogging_CanRead'
)