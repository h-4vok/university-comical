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
SELECT
	Code = data.Code
FROM (

	SELECT Code = 'User_New' UNION
	SELECT Code = 'User_Update' UNION
	SELECT Code = 'User_Disable' UNION
	SELECT Code = 'User_Enable' UNION
	SELECT Code = 'User_Delete' UNION
	SELECT Code = 'User_Read' UNION
	SELECT Code = 'ShoppingCart_CanUse' UNION
	SELECT Code = 'Account_CanUse' UNION
	SELECT Code = 'InfoLogging_CanRead' UNION
	SELECT Code = 'ErrorLogging_CanRead' UNION
	SELECT Code = 'VerifierDigits_CheckOnLogin' UNION
	SELECT Code = 'UnderMaintenance_CanLogin' UNION
	SELECT Code = 'HasChecksumError_CanLogin' UNION
	SELECT Code = 'VerifierDigits_CanFix'

	) DATA
LEFT  JOIN	Permission P
		ON	data.Code = p.Code

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
	Blocked = 0
FROM		(
				SELECT Login = 'admin', Password = 'fIdUH9Pz71AW4S1BGQDIemBGqOg=' UNION
				SELECT Login = 'test', Password = 'fIdUH9Pz71AW4S1BGQDIemBGqOg='
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

-- Setup roles for ADMIN user
INSERT UserRole (
	UserId,
	RoleId
)
SELECT
	UserId = data.UserId,
	RoleId = data.RoleId
FROM (
	SELECT
		UserId = u.Id,
		RoleId = r.Id
	FROM		[User] U, Role R
	WHERE		u.Login = 'admin'
	AND			r.Code = 'admin'
) DATA

LEFT  JOIN	UserRole UR
		ON	data.UserId = ur.UserId
		AND	data.RoleID = ur.RoleId

WHERE		ur.UserId IS NULL