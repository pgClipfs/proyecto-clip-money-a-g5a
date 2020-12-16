CREATE DATABASE ClipMoney
go

USE ClipMoney
go

--Tablas extremos uno
CREATE TABLE TIPO_CUENTAS
(
	ID_TIPO_CUENTA TINYINT IDENTITY,
	TIPO_CUENTA VARCHAR(50) NOT NULL,
	CONSTRAINT PK_TIPO_CUENTA PRIMARY KEY (ID_TIPO_CUENTA)
)

GO

CREATE TABLE TIPO_SERVICIOS
(
	ID_TIPO_SERVICIO TINYINT IDENTITY, 
	SERVICIO VARCHAR(50) NOT NULL,
	CONSTRAINT PK_TIPO_SERVICIO PRIMARY KEY (ID_TIPO_SERVICIO)
)

GO

CREATE TABLE TIPO_TRANSACCIONES	--Compra y Venta de moneda extranjera
(
	ID_TIPO_TRANSACCION TINYINT IDENTITY,
	TIPO_TRANSACCION VARCHAR(50) NOT NULL,
	CONSTRAINT PK_TIPO_TRANSACCION PRIMARY KEY (ID_TIPO_TRANSACCION)
)

GO

CREATE TABLE TIPO_INVERSIONES
(
	ID_TIPO_INVERSION TINYINT IDENTITY,
	INVERSION VARCHAR(50) NOT NULL,	-- plazo fijo, fci
	CONSTRAINT PK_TIPO_INVERSIONES PRIMARY KEY (ID_TIPO_INVERSION)
)
GO
CREATE TABLE DIVISAS
(
	ID_DIVISA TINYINT IDENTITY,
	DIVISA VARCHAR(50) NOT NULL,
 	COMISION FLOAT, -- iria aca o en las transacciones?
	PRECIO_COMPRA SMALLMONEY NOT NULL,
	PRECIO_VENTA SMALLMONEY NOT NULL,
	CONSTRAINT PK_DIVISA PRIMARY KEY (ID_DIVISA)
)

go

CREATE TABLE SITUACIONES_CREDITICIAS
(
	ID_SITUACION_CREDITICIA TINYINT IDENTITY,
	SITUACION VARCHAR(50) NOT NULL,
	MAX_GIRO_DESCUBIERTO SMALLMONEY NOT NULL,	--en que moneda estaria esto?
	CONSTRAINT PK_SITUACION_CREDITICIA PRIMARY KEY (ID_SITUACION_CREDITICIA)
)


GO

--Tablas con foreign keys
CREATE TABLE USUARIOS
(
	ID_USUARIO INT IDENTITY,
	CUIL CHAR(11) NOT NULL,
	NOMBRE VARCHAR(50) NOT NULL,
	APELLIDO VARCHAR(50) NOT NULL,
	CONTRASE�A VARCHAR(50) NOT NULL,	--encriptar
	EMAIL VARCHAR(40) NOT NULL,
	TELEFONO CHAR(11) NOT NULL,
	ID_SITUACION_CREDITICIA TINYINT,
	PRIVILEGIOS VARCHAR(15) NOT NULL,
	CONSTRAINT PK_USUARIOS PRIMARY KEY (ID_USUARIO),
	CONSTRAINT FK_CLIENTES_SITUACIONES_CREDITICIAS FOREIGN KEY (ID_SITUACION_CREDITICIA) REFERENCES SITUACIONES_CREDITICIAS (ID_SITUACION_CREDITICIA)
)

GO

CREATE TABLE CUENTAS
(
	ID_CUENTA INT IDENTITY, 
	ID_TIPO_CUENTA TINYINT NOT NULL,
	ID_DIVISA TINYINT,
	ID_USUARIO INT,
	CVU CHAR(22) NOT NULL,
	SALDO MONEY NOT NULL,
	ALIAS VARCHAR(20),
	FECHA_APERTURA DATE,
	CONSTRAINT PK_CUENTAS PRIMARY KEY (ID_CUENTA),
	CONSTRAINT FK_CUENTAS_USUARIOS FOREIGN KEY (ID_USUARIO) REFERENCES USUARIOS (ID_USUARIO),
	CONSTRAINT FK_CUENTAS_TIPO_CUENTAS FOREIGN KEY (ID_TIPO_CUENTA) REFERENCES TIPO_CUENTAS (ID_TIPO_CUENTA),
	CONSTRAINT FK_CUENTAS_DIVISAS FOREIGN KEY (ID_DIVISA) REFERENCES DIVISAS (ID_DIVISA)
)

GO
CREATE TABLE TRANSACCIONES
(
	ID_TRANSACCION INT IDENTITY,
	ID_CUENTA_ORIGEN INT NOT NULL,
	ID_CUENTA_DESTINO INT NOT NULL,
	ID_TIPO_TRANSACCION TINYINT NOT NULL,
	ID_DIVISA TINYINT NOT NULL,
	FECHA SMALLDATETIME NOT NULL,
	CANTIDAD MONEY NOT NULL,
	PRECIO SMALLMONEY NOT NULL,	--al momento de la operacion, debe estar en la divisa de la cuenta emisora(pq es la que paga)
	COMISION FLOAT NOT NULL, --El %, no el total
	CONSTRAINT PK_TRANSACCIONES PRIMARY KEY (ID_TRANSACCION),
	CONSTRAINT FK_TRANSACCIONES_CUENTAS_ORIGEN FOREIGN KEY (ID_CUENTA_ORIGEN) REFERENCES CUENTAS (ID_CUENTA),
	CONSTRAINT FK_TRANSACCIONES_CUENTAS_DESTINO FOREIGN KEY (ID_CUENTA_DESTINO) REFERENCES CUENTAS (ID_CUENTA),
	CONSTRAINT FK_TRANSACCIONES_TIPO_TRANSACCIONES FOREIGN KEY (ID_TIPO_TRANSACCION) REFERENCES TIPO_TRANSACCIONES (ID_TIPO_TRANSACCION),
	CONSTRAINT FK_TRANSACCIONES_DIVISAS FOREIGN KEY (ID_DIVISA) REFERENCES DIVISAS (ID_DIVISA)
)

go

CREATE TABLE TRANSFERENCIAS	-- la cuenta que recibe debe ser de la misma divisa que la cuenta emisora? o se puede hacer una operatoria de cambio de moneda de por medio
(
	ID_TRANSFERENCIA INT IDENTITY, 
	ID_CUENTA_ORIGEN INT NOT NULL,
	ID_CUENTA_DESTINO INT NOT NULL,	--si la cuenta est� fuera de este banco no estar�a en nuestra tabla de cuentas... podria no relacionarse este campo y poder poner sarasa?)
	ID_DIVISA TINYINT,
	FECHA SMALLDATETIME NOT NULL,
	MONTO MONEY NOT NULL,
	CONSTRAINT PK_TRANSFERENCIAS PRIMARY KEY (ID_TRANSFERENCIA),
	CONSTRAINT FK_TRANSFERENCIAS_DIVISAS FOREIGN KEY (ID_DIVISA) REFERENCES DIVISAS (ID_DIVISA),
	CONSTRAINT FK_TRANSFERENCIAS_CUENTAS_ORIGEN FOREIGN KEY (ID_CUENTA_ORIGEN) REFERENCES CUENTAS (ID_CUENTA),
	CONSTRAINT FK_TRANSFERENCIAS_CUENTAS_DESTINO FOREIGN KEY (ID_CUENTA_DESTINO) REFERENCES CUENTAS (ID_CUENTA),
)

--TIPO_TRANSACCIONES --> nacional_mismoBanco, nacional_otroBanco, Internacional

go
CREATE TABLE INVERSIONES
(
	ID_INVERSION INT IDENTITY,
	ID_TIPO_INVERSION TINYINT NOT NULL,
	ID_CUENTA INT NOT NULL,
	FECHA DATE NOT NULL,
	MONTO SMALLMONEY NOT NULL,
	INTERES FLOAT,	--lo dejo anulable pq en el caso de un fci no sabes los interesas ganados hasta que sacas la plata de ah�
	PLAZO INT, --idem anterior
	CONSTRAINT PK_INVERSIONES PRIMARY KEY (ID_INVERSION),
	CONSTRAINT FK_INVERSIONES_TIPO_INVERSION FOREIGN KEY (ID_TIPO_INVERSION) REFERENCES TIPO_INVERSIONES (ID_TIPO_INVERSION),
	CONSTRAINT FK_INVERSIONES_CUENTAS FOREIGN KEY (ID_CUENTA) REFERENCES CUENTAS(ID_CUENTA)
)



--!!!!!!!!!!!!
--Probablemente haya que hacer cambios en las tablas de servicios y facturas, veremos que pasa cuando haya que implementarla. Quiza una BD aparte
CREATE TABLE SERVICIOS
(
	ID_SERVICIO SMALLINT IDENTITY,
	ID_TIPO_SERVICIO TINYINT,
	SERVICIO VARCHAR(50),
	CONSTRAINT PK_SERVICIOS PRIMARY KEY (ID_SERVICIO),
	CONSTRAINT FK_SERVICIOS_TIPO_SERVICIOS FOREIGN KEY (ID_TIPO_SERVICIO) REFERENCES TIPO_SERVICIOS (ID_TIPO_SERVICIO)
)

GO

CREATE TABLE FACTURAS		-- Capaz haga falta agregar un estado de la factura, paga/impaga
(
	ID_FACTURA INT IDENTITY,
	ID_SERVICIO SMALLINT NOT NULL,
	FECHA_PAGO DATE,
	FECHA_VENCIMIENTO DATE NOT NULL,
	MONTO SMALLMONEY NOT NULL,
	ID_CUENTA INT NOT NULL,
	--DETALLE/DESCRIPCION
	CONSTRAINT PK_FACTURAS PRIMARY KEY (ID_FACTURA),
	CONSTRAINT FK_FACTURAS_SERVICIOS FOREIGN KEY (ID_SERVICIO) REFERENCES SERVICIOS (ID_SERVICIO),
	CONSTRAINT FK_FACTURAS_CUENTAS FOREIGN KEY (ID_CUENTA) REFERENCES CUENTAS (ID_CUENTA) 
)


--Actualizaci�n 15 de Octubre 2020

--DATOS DE LAS TABLAS