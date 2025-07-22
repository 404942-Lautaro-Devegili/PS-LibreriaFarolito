USE [master]
GO
/****** Object:  Database [LibreriaFarolito]    Script Date: 22/7/2025 15:58:59 ******/
CREATE DATABASE [LibreriaFarolito]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'LibreriaFarolito', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\LibreriaFarolito.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'LibreriaFarolito_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\LibreriaFarolito_log.ldf' , SIZE = 663552KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [LibreriaFarolito] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [LibreriaFarolito].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [LibreriaFarolito] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [LibreriaFarolito] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [LibreriaFarolito] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [LibreriaFarolito] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [LibreriaFarolito] SET ARITHABORT OFF 
GO
ALTER DATABASE [LibreriaFarolito] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [LibreriaFarolito] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [LibreriaFarolito] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [LibreriaFarolito] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [LibreriaFarolito] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [LibreriaFarolito] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [LibreriaFarolito] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [LibreriaFarolito] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [LibreriaFarolito] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [LibreriaFarolito] SET  ENABLE_BROKER 
GO
ALTER DATABASE [LibreriaFarolito] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [LibreriaFarolito] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [LibreriaFarolito] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [LibreriaFarolito] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [LibreriaFarolito] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [LibreriaFarolito] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [LibreriaFarolito] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [LibreriaFarolito] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [LibreriaFarolito] SET  MULTI_USER 
GO
ALTER DATABASE [LibreriaFarolito] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [LibreriaFarolito] SET DB_CHAINING OFF 
GO
ALTER DATABASE [LibreriaFarolito] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [LibreriaFarolito] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [LibreriaFarolito] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [LibreriaFarolito] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [LibreriaFarolito] SET QUERY_STORE = ON
GO
ALTER DATABASE [LibreriaFarolito] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [LibreriaFarolito]
GO
/****** Object:  User [farolitoOwner]    Script Date: 22/7/2025 15:58:59 ******/
CREATE USER farolitoOwner FOR LOGIN [admin] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [farolitoOwner]
GO
/****** Object:  UserDefinedTableType [dbo].[CodigoTableType]    Script Date: 22/7/2025 15:58:59 ******/
CREATE TYPE [dbo].[CodigoTableType] AS TABLE(
	[Codigo] [int] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[DescripcionTableType]    Script Date: 22/7/2025 15:58:59 ******/
CREATE TYPE [dbo].[DescripcionTableType] AS TABLE(
	[Descripcion] [varchar](200) NULL
)
GO
/****** Object:  Table [dbo].[autores]    Script Date: 22/7/2025 15:58:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[autores](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[autor] [nvarchar](200) NULL,
 CONSTRAINT [PK_AUTORES] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[clientes]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[clientes](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[codigo] [int] NULL,
	[nombre] [nvarchar](150) NULL,
	[direccion] [nvarchar](500) NULL,
	[id_localidad] [int] NULL,
	[telefono] [nvarchar](30) NULL,
	[email] [nvarchar](500) NULL,
 CONSTRAINT [PK_CLIENTES] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[editoriales]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[editoriales](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[editorial] [nvarchar](200) NULL,
 CONSTRAINT [PK_EDITORIALES] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[formaspago]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[formaspago](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[formapago] [nvarchar](200) NULL,
	[porcentaje_recargo] [decimal](5, 2) NULL,
	[senia_minima] [decimal](5, 2) NULL,
 CONSTRAINT [PK_FORMASPAGO] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[libros]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[libros](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[codigo] [int] NULL,
	[isbn] [varchar](100) NULL,
	[ean13] [varchar](100) NULL,
	[titulo] [varchar](300) NULL,
	[id_autor] [int] NULL,
	[id_serie] [int] NULL,
	[id_editorial] [int] NULL,
	[id_materia] [int] NULL,
	[precioLista] [decimal](10, 2) NULL,
	[precio] [decimal](10, 2) NULL,
	[contado] [decimal](10, 2) NULL,
	[existencia] [int] NULL,
	[novedad] [bit] NULL,
	[disponible] [bit] NULL,
	[fec_mod] [datetime] NULL,
	[cargadoUltimaVezPorExcel] [bit] NULL,
 CONSTRAINT [PK_LIBROS] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[localidades]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[localidades](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[localidad] [nvarchar](200) NULL,
	[id_provincia] [int] NULL,
 CONSTRAINT [PK_LOCALIDADES] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[materias]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[materias](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[materia] [nvarchar](200) NULL,
 CONSTRAINT [PK_MATERIAS] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[pagos]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pagos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[monto] [decimal](10, 2) NOT NULL,
	[metodo_pago] [nvarchar](50) NOT NULL,
	[estado_pago] [nvarchar](20) NOT NULL,
	[fecha_pago] [datetime] NOT NULL,
	[preference_id] [nvarchar](200) NULL,
	[payment_id] [nvarchar](200) NULL,
	[external_reference] [nvarchar](500) NULL,
	[reservas_ids] [nvarchar](1000) NOT NULL,
	[activo] [bit] NOT NULL,
 CONSTRAINT [PK_PAGOS] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[pedidos]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pedidos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_libro] [int] NULL,
	[id_proveedor] [int] NULL,
	[cantidad] [int] NULL,
	[fchpedido] [datetime] NULL,
	[procesando] [bit] NULL,
	[enviado] [bit] NULL,
	[recibido] [bit] NULL,
 CONSTRAINT [PK_PEDIDOS] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[precios]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[precios](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_libro] [int] NOT NULL,
	[precioLista] [decimal](10, 2) NULL,
	[precio] [decimal](10, 2) NULL,
	[contado] [decimal](10, 2) NULL,
	[fchDesde] [datetime] NULL,
	[fchHasta] [datetime] NULL,
	[fchCargado] [datetime] NULL,
	[cargadoPorExcel] [bit] NULL,
	[usuarioQueModifico] [int] NULL,
 CONSTRAINT [PK_PRECIOS] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[proveedores]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[proveedores](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[proveedor] [nvarchar](150) NULL,
	[direccion] [nvarchar](500) NULL,
	[id_localidad] [int] NULL,
	[cod_postal] [int] NULL,
	[telefono] [nvarchar](30) NULL,
	[nro_iva] [nvarchar](150) NULL,
	[contacto] [varchar](200) NULL,
	[email] [varchar](200) NULL,
	[observaciones] [varchar](1000) NULL,
	[activo] [bit] NOT NULL,
 CONSTRAINT [PK_PROVEEDORES] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[provincias]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[provincias](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[provincia] [nvarchar](200) NULL,
 CONSTRAINT [PK_PROVINCIAS] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[reservas]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[reservas](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_libro] [int] NULL,
	[id_cliente] [int] NULL,
	[id_proveedor] [int] NULL,
	[senia] [money] NULL,
	[saldo] [money] NULL,
	[fchreserva] [datetime] NULL,
	[fchentrega] [datetime] NULL,
	[id_forma_pago] [int] NULL,
	[libroPedido] [bit] NULL,
	[asignada] [bit] NULL,
	[entregada] [bit] NULL,
	[activa] [bit] NULL,
 CONSTRAINT [PK_RESERVAS] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[roles]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[roles](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](100) NOT NULL,
	[descripcion] [nvarchar](250) NOT NULL,
	[estado] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[series]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[series](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[serie] [nvarchar](200) NULL,
 CONSTRAINT [PK_SERIES] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[usuarios]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usuarios](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](100) NULL,
	[apellido] [nvarchar](100) NULL,
	[usuario] [nvarchar](100) NULL,
	[pass] [nvarchar](100) NULL,
	[activo] [bit] NOT NULL,
	[fechaMod] [datetime] NOT NULL,
	[idRol] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[formaspago] ADD  DEFAULT ((0)) FOR [porcentaje_recargo]
GO
ALTER TABLE [dbo].[formaspago] ADD  DEFAULT ((0)) FOR [senia_minima]
GO
ALTER TABLE [dbo].[pagos] ADD  DEFAULT (getdate()) FOR [fecha_pago]
GO
ALTER TABLE [dbo].[pagos] ADD  DEFAULT ((1)) FOR [activo]
GO
ALTER TABLE [dbo].[pedidos] ADD  DEFAULT ((0)) FOR [procesando]
GO
ALTER TABLE [dbo].[pedidos] ADD  DEFAULT ((0)) FOR [enviado]
GO
ALTER TABLE [dbo].[pedidos] ADD  DEFAULT ((0)) FOR [recibido]
GO
ALTER TABLE [dbo].[proveedores] ADD  DEFAULT ((1)) FOR [activo]
GO
ALTER TABLE [dbo].[clientes]  WITH CHECK ADD  CONSTRAINT [FK_CLIENTES_LOCALIDADES] FOREIGN KEY([id_localidad])
REFERENCES [dbo].[localidades] ([id])
GO
ALTER TABLE [dbo].[clientes] CHECK CONSTRAINT [FK_CLIENTES_LOCALIDADES]
GO
ALTER TABLE [dbo].[libros]  WITH CHECK ADD  CONSTRAINT [FK_LIBROS_AUTORES] FOREIGN KEY([id_autor])
REFERENCES [dbo].[autores] ([id])
GO
ALTER TABLE [dbo].[libros] CHECK CONSTRAINT [FK_LIBROS_AUTORES]
GO
ALTER TABLE [dbo].[libros]  WITH CHECK ADD  CONSTRAINT [FK_LIBROS_EDITORIALES] FOREIGN KEY([id_editorial])
REFERENCES [dbo].[editoriales] ([id])
GO
ALTER TABLE [dbo].[libros] CHECK CONSTRAINT [FK_LIBROS_EDITORIALES]
GO
ALTER TABLE [dbo].[libros]  WITH CHECK ADD  CONSTRAINT [FK_LIBROS_MATERIAS] FOREIGN KEY([id_materia])
REFERENCES [dbo].[materias] ([id])
GO
ALTER TABLE [dbo].[libros] CHECK CONSTRAINT [FK_LIBROS_MATERIAS]
GO
ALTER TABLE [dbo].[libros]  WITH CHECK ADD  CONSTRAINT [FK_LIBROS_SERIES] FOREIGN KEY([id_serie])
REFERENCES [dbo].[series] ([id])
GO
ALTER TABLE [dbo].[libros] CHECK CONSTRAINT [FK_LIBROS_SERIES]
GO
ALTER TABLE [dbo].[localidades]  WITH CHECK ADD  CONSTRAINT [FK_LOCALIDADES_PROVINCIAS] FOREIGN KEY([id_provincia])
REFERENCES [dbo].[provincias] ([id])
GO
ALTER TABLE [dbo].[localidades] CHECK CONSTRAINT [FK_LOCALIDADES_PROVINCIAS]
GO
ALTER TABLE [dbo].[pedidos]  WITH CHECK ADD  CONSTRAINT [FK_PEDIDOS_LIBROS] FOREIGN KEY([id_libro])
REFERENCES [dbo].[libros] ([id])
GO
ALTER TABLE [dbo].[pedidos] CHECK CONSTRAINT [FK_PEDIDOS_LIBROS]
GO
ALTER TABLE [dbo].[pedidos]  WITH CHECK ADD  CONSTRAINT [FK_PEDIDOS_PROVEEDORES] FOREIGN KEY([id_proveedor])
REFERENCES [dbo].[proveedores] ([id])
GO
ALTER TABLE [dbo].[pedidos] CHECK CONSTRAINT [FK_PEDIDOS_PROVEEDORES]
GO
ALTER TABLE [dbo].[precios]  WITH CHECK ADD  CONSTRAINT [FK_PRECIOS_LIBROS] FOREIGN KEY([id_libro])
REFERENCES [dbo].[libros] ([id])
GO
ALTER TABLE [dbo].[precios] CHECK CONSTRAINT [FK_PRECIOS_LIBROS]
GO
ALTER TABLE [dbo].[proveedores]  WITH CHECK ADD  CONSTRAINT [FK_PROVEEDORES_LOCALIDADES] FOREIGN KEY([id_localidad])
REFERENCES [dbo].[localidades] ([id])
GO
ALTER TABLE [dbo].[proveedores] CHECK CONSTRAINT [FK_PROVEEDORES_LOCALIDADES]
GO
ALTER TABLE [dbo].[reservas]  WITH CHECK ADD  CONSTRAINT [FK_RESERVAS_CLIENTES] FOREIGN KEY([id_cliente])
REFERENCES [dbo].[clientes] ([id])
GO
ALTER TABLE [dbo].[reservas] CHECK CONSTRAINT [FK_RESERVAS_CLIENTES]
GO
ALTER TABLE [dbo].[reservas]  WITH CHECK ADD  CONSTRAINT [FK_RESERVAS_FORMASPAGO] FOREIGN KEY([id_forma_pago])
REFERENCES [dbo].[formaspago] ([id])
GO
ALTER TABLE [dbo].[reservas] CHECK CONSTRAINT [FK_RESERVAS_FORMASPAGO]
GO
ALTER TABLE [dbo].[reservas]  WITH CHECK ADD  CONSTRAINT [FK_RESERVAS_LIBROS] FOREIGN KEY([id_libro])
REFERENCES [dbo].[libros] ([id])
GO
ALTER TABLE [dbo].[reservas] CHECK CONSTRAINT [FK_RESERVAS_LIBROS]
GO
ALTER TABLE [dbo].[reservas]  WITH CHECK ADD  CONSTRAINT [FK_RESERVAS_PROVEEDORES] FOREIGN KEY([id_proveedor])
REFERENCES [dbo].[proveedores] ([id])
GO
ALTER TABLE [dbo].[reservas] CHECK CONSTRAINT [FK_RESERVAS_PROVEEDORES]
GO
ALTER TABLE [dbo].[usuarios]  WITH CHECK ADD FOREIGN KEY([idRol])
REFERENCES [dbo].[roles] ([id])
GO
/****** Object:  StoredProcedure [dbo].[SP_ACTUALIZAR_FORMA_PAGO_COMPLETO]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_ACTUALIZAR_FORMA_PAGO_COMPLETO]
(
    @PID_FORMA_PAGO INT,
    @PPORCENTAJE_RECARGO DECIMAL(5,2),
    @PSENIA_MINIMA DECIMAL(5,2)
)
AS
BEGIN
    UPDATE formaspago 
    SET porcentaje_recargo = @PPORCENTAJE_RECARGO,
        senia_minima = @PSENIA_MINIMA
    WHERE id = @PID_FORMA_PAGO;
    
    SELECT @@ROWCOUNT;
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_ACTUALIZAR_PORCENTAJE_FORMA_PAGO]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_ACTUALIZAR_PORCENTAJE_FORMA_PAGO]
(
    @PID_FORMA_PAGO INT,
    @PPORCENTAJE_RECARGO DECIMAL(5,2)
)
AS
BEGIN
    UPDATE formaspago 
    SET porcentaje_recargo = @PPORCENTAJE_RECARGO 
    WHERE id = @PID_FORMA_PAGO;
    
    SELECT @@ROWCOUNT;
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_AGREGARPROVEDOR]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_AGREGARPROVEDOR]
(
    @PNOMBRE VARCHAR(200),
    @PCONTACTO VARCHAR(200) = NULL,
    @PTELEFONO VARCHAR(50) = NULL,
    @PEMAIL VARCHAR(200) = NULL,
    @PDIRECCION VARCHAR(500) = NULL,
    @POBSERVACIONES VARCHAR(1000) = NULL,
    @PACTIVO BIT = 1
)
AS
BEGIN
    INSERT INTO proveedores
    (
        proveedor, contacto, telefono, email, 
        direccion, observaciones, activo
    )
    VALUES 
    (
        @PNOMBRE, @PCONTACTO, @PTELEFONO, @PEMAIL,
        @PDIRECCION, @POBSERVACIONES, @PACTIVO
    );
    
    SELECT SCOPE_IDENTITY() as IdGenerado;
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_ANULARRESERVA]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_ANULARRESERVA]
(
  @PIDRESERVATRAER INT OUTPUT,
  @PIDRESERVA INT
)
AS
BEGIN
  DECLARE @UpdatedTable TABLE (id INT);

  UPDATE reservas
  SET activa = 0
  OUTPUT INSERTED.id INTO @UpdatedTable
  WHERE id = @PIDRESERVA;

  SELECT @PIDRESERVATRAER = id FROM @UpdatedTable;
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_ASIGNARRESERVA]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_ASIGNARRESERVA]
(
  @PIDRESERVATRAER INT OUTPUT,
  @PIDRESERVA INT
)
AS
BEGIN
  DECLARE @UpdatedTable TABLE (id INT);

  UPDATE reservas
  SET asignada = 1
  OUTPUT INSERTED.id INTO @UpdatedTable
  WHERE id = @PIDRESERVA;

  SELECT @PIDRESERVATRAER = id FROM @UpdatedTable;
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_ENTREGARRESERVA]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_ENTREGARRESERVA]
(
  @PIDRESERVATRAER INT OUTPUT,
  @PIDRESERVA INT
)
AS
BEGIN
  DECLARE @UpdatedTable TABLE (id INT);

  UPDATE reservas
  SET entregada = 1,
  saldo = 0,
  fchentrega = getdate()
  OUTPUT INSERTED.id INTO @UpdatedTable
  WHERE id = @PIDRESERVA;

  SELECT @PIDRESERVATRAER = id FROM @UpdatedTable;
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_ESTAENBD]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_ESTAENBD]
(
  @AUTOROMATERIAOEDITORIAL VARCHAR(500),
  @OPCION VARCHAR(20),
  @ENBD INT OUT
)
AS
BEGIN
  IF @OPCION = 'A'
  BEGIN
    DECLARE @ResultA INT;
	SET @ResultA = 0;
    SELECT @ResultA = 1 FROM autores where autor = @AUTOROMATERIAOEDITORIAL;
    SET @ENBD = @ResultA;
  END
  ELSE IF @OPCION = 'E'
  BEGIN
    DECLARE @ResultE INT;
	SET @ResultE = 0;
    SELECT @ResultE = 1 FROM editoriales where editorial = @AUTOROMATERIAOEDITORIAL;
    SET @ENBD = @ResultE;
  END
  ELSE IF @OPCION = 'M'
  BEGIN
    DECLARE @ResultM INT;
	SET @ResultM = 0;
    SELECT @ResultM = 1 FROM materias WHERE materia = @AUTOROMATERIAOEDITORIAL;
    SET @ENBD = @ResultM;
  END
  ELSE IF @OPCION = 'S'
  BEGIN
    DECLARE @ResultS INT;
	SET @ResultS = 0;
    SELECT @ResultS = 1 FROM series WHERE serie = @AUTOROMATERIAOEDITORIAL;
    SET @ENBD = @ResultS;
  END
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_GET_DETALLE_PEDIDOS_ESTADISTICAS]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GET_DETALLE_PEDIDOS_ESTADISTICAS]
(
    @PDISTRIBUIDOR_ID INT = -1,
    @PFECHA_INICIO DATETIME = NULL,
    @PFECHA_FIN DATETIME = NULL
)
AS
BEGIN
    SELECT 
        p.id,
        pr.proveedor AS distribuidor,
        p.fchpedido AS fecha,
        p.cantidad AS total_libros,
        (l.precio * p.cantidad) AS monto,
        CASE 
            WHEN p.recibido = 1 THEN 'Recibido'
            WHEN p.enviado = 1 THEN 'Enviado'
            WHEN p.procesando = 1 THEN 'Procesando'
            ELSE 'Pendiente'
        END AS estado
    FROM pedidos p
    INNER JOIN libros l ON p.id_libro = l.id
    INNER JOIN proveedores pr ON p.id_proveedor = pr.id
    WHERE 
        (@PDISTRIBUIDOR_ID = -1 OR p.id_proveedor = @PDISTRIBUIDOR_ID)
        AND (@PFECHA_INICIO IS NULL OR p.fchpedido >= @PFECHA_INICIO)
        AND (@PFECHA_FIN IS NULL OR p.fchpedido <= @PFECHA_FIN)
    ORDER BY p.fchpedido DESC;
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_GET_DETALLE_RESERVAS_ESTADISTICAS]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GET_DETALLE_RESERVAS_ESTADISTICAS]
(
    @PMETODO_PAGO_ID INT = -1,
    @PFECHA_INICIO DATETIME = NULL,
    @PFECHA_FIN DATETIME = NULL
)
AS
BEGIN
    SELECT 
        r.id,
        c.nombre AS cliente,
        fp.formapago AS metodo_pago,
        r.fchreserva AS fecha,
        (r.senia + r.saldo) AS monto,
        CASE 
            WHEN r.entregada = 1 THEN 'Entregada'
            WHEN r.asignada = 1 THEN 'Asignada'
            WHEN r.libroPedido = 1 THEN 'Pedida'
            WHEN r.activa = 0 THEN 'Cancelada'
            ELSE 'Sin Asignar'
        END AS estado
    FROM reservas r
    INNER JOIN clientes c ON r.id_cliente = c.id
    INNER JOIN formaspago fp ON r.id_forma_pago = fp.id
    WHERE 
        r.activa = 1
        AND (@PMETODO_PAGO_ID = -1 OR r.id_forma_pago = @PMETODO_PAGO_ID)
        AND (@PFECHA_INICIO IS NULL OR r.fchreserva >= @PFECHA_INICIO)
        AND (@PFECHA_FIN IS NULL OR r.fchreserva <= @PFECHA_FIN)
    ORDER BY r.fchreserva DESC;
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_GET_ESTADISTICAS_GENERALES_PEDIDOS]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GET_ESTADISTICAS_GENERALES_PEDIDOS]
(
    @PDISTRIBUIDOR_ID INT = -1,
    @PFECHA_INICIO DATETIME = NULL,
    @PFECHA_FIN DATETIME = NULL
)
AS
BEGIN
    DECLARE @total_pedidos INT = 0;
    DECLARE @monto_total DECIMAL(18,2) = 0;
    DECLARE @promedio DECIMAL(18,2) = 0;
    DECLARE @ultimo_mes INT = 0;

    SELECT 
        @total_pedidos = COUNT(DISTINCT p.id),
        @monto_total = ISNULL(SUM(l.precio * p.cantidad), 0)
    FROM pedidos p
    INNER JOIN libros l ON p.id_libro = l.id
    INNER JOIN proveedores pr ON p.id_proveedor = pr.id
    WHERE 
        (@PDISTRIBUIDOR_ID = -1 OR p.id_proveedor = @PDISTRIBUIDOR_ID)
        AND (@PFECHA_INICIO IS NULL OR p.fchpedido >= @PFECHA_INICIO)
        AND (@PFECHA_FIN IS NULL OR p.fchpedido <= @PFECHA_FIN);

    IF @total_pedidos > 0
        SET @promedio = @monto_total / @total_pedidos;

    SELECT @ultimo_mes = COUNT(DISTINCT p.id)
    FROM pedidos p
    INNER JOIN proveedores pr ON p.id_proveedor = pr.id
    WHERE 
        (@PDISTRIBUIDOR_ID = -1 OR p.id_proveedor = @PDISTRIBUIDOR_ID)
        AND p.fchpedido >= DATEADD(MONTH, -1, GETDATE());

    SELECT 
        @total_pedidos AS total_pedidos,
        @monto_total AS monto_total,
        @promedio AS promedio,
        @ultimo_mes AS ultimo_mes;
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_GET_ESTADISTICAS_GENERALES_RESERVAS]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GET_ESTADISTICAS_GENERALES_RESERVAS]
(
    @PMETODO_PAGO_ID INT = -1,
    @PFECHA_INICIO DATETIME = NULL,
    @PFECHA_FIN DATETIME = NULL
)
AS
BEGIN
    DECLARE @total_reservas INT = 0;
    DECLARE @monto_total DECIMAL(18,2) = 0;
    DECLARE @promedio DECIMAL(18,2) = 0;
    DECLARE @ultimo_mes INT = 0;

    SELECT 
        @total_reservas = COUNT(r.id),
        @monto_total = ISNULL(SUM(r.senia + r.saldo), 0)
    FROM reservas r
    INNER JOIN formaspago fp ON r.id_forma_pago = fp.id
    WHERE 
        r.activa = 1
        AND (@PMETODO_PAGO_ID = -1 OR r.id_forma_pago = @PMETODO_PAGO_ID)
        AND (@PFECHA_INICIO IS NULL OR r.fchreserva >= @PFECHA_INICIO)
        AND (@PFECHA_FIN IS NULL OR r.fchreserva <= @PFECHA_FIN);

    IF @total_reservas > 0
        SET @promedio = @monto_total / @total_reservas;

    SELECT @ultimo_mes = COUNT(r.id)
    FROM reservas r
    INNER JOIN formaspago fp ON r.id_forma_pago = fp.id
    WHERE 
        r.activa = 1
        AND (@PMETODO_PAGO_ID = -1 OR r.id_forma_pago = @PMETODO_PAGO_ID)
        AND r.fchreserva >= DATEADD(MONTH, -1, GETDATE());

    SELECT 
        @total_reservas AS total_reservas,
        @monto_total AS monto_total,
        @promedio AS promedio,
        @ultimo_mes AS ultimo_mes;
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_GET_TENDENCIAS_PEDIDOS]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GET_TENDENCIAS_PEDIDOS]
(
    @PDISTRIBUIDOR_ID INT = -1,
    @PFECHA_INICIO DATETIME = NULL,
    @PFECHA_FIN DATETIME = NULL
)
AS
BEGIN
    IF @PFECHA_INICIO IS NULL
        SET @PFECHA_INICIO = DATEADD(DAY, -30, GETDATE());
    
    IF @PFECHA_FIN IS NULL
        SET @PFECHA_FIN = GETDATE();

    SELECT 
        CAST(p.fchpedido AS DATE) AS fecha,
        COUNT(DISTINCT p.id) AS cantidad,
        ISNULL(SUM(l.precio * p.cantidad), 0) AS monto
    FROM pedidos p
    INNER JOIN libros l ON p.id_libro = l.id
    INNER JOIN proveedores pr ON p.id_proveedor = pr.id
    WHERE 
        (@PDISTRIBUIDOR_ID = -1 OR p.id_proveedor = @PDISTRIBUIDOR_ID)
        AND p.fchpedido >= @PFECHA_INICIO
        AND p.fchpedido <= @PFECHA_FIN
    GROUP BY CAST(p.fchpedido AS DATE)
    ORDER BY CAST(p.fchpedido AS DATE);
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_GET_TENDENCIAS_RESERVAS]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GET_TENDENCIAS_RESERVAS]
(
    @PMETODO_PAGO_ID INT = -1,
    @PFECHA_INICIO DATETIME = NULL,
    @PFECHA_FIN DATETIME = NULL
)
AS
BEGIN
    IF @PFECHA_INICIO IS NULL
        SET @PFECHA_INICIO = DATEADD(DAY, -30, GETDATE());
    
    IF @PFECHA_FIN IS NULL
        SET @PFECHA_FIN = GETDATE();

    SELECT 
        CAST(r.fchreserva AS DATE) AS fecha,
        COUNT(r.id) AS cantidad,
        ISNULL(SUM(r.senia + r.saldo), 0) AS monto
    FROM reservas r
    INNER JOIN formaspago fp ON r.id_forma_pago = fp.id
    WHERE 
        r.activa = 1
        AND (@PMETODO_PAGO_ID = -1 OR r.id_forma_pago = @PMETODO_PAGO_ID)
        AND r.fchreserva >= @PFECHA_INICIO
        AND r.fchreserva <= @PFECHA_FIN
    GROUP BY CAST(r.fchreserva AS DATE)
    ORDER BY CAST(r.fchreserva AS DATE);
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_GETALLAUTORES]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GETALLAUTORES]
AS
BEGIN
    SELECT a.id id,
	       a.autor autor
    FROM autores a;
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_GETALLEDITORIALES]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GETALLEDITORIALES]
AS
BEGIN
    SELECT e.id id,
	       e.editorial editorial
    FROM editoriales e;
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_GETALLMATERIAS]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GETALLMATERIAS]
AS
BEGIN
    SELECT m.id id,
	       m.materia materia
    FROM materias m;
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_GETALLSERIES]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GETALLSERIES]
AS
BEGIN
    SELECT s.id id,
	       s.serie serie
    FROM series s;
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_GETAUTORES]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GETAUTORES]
AS
BEGIN
	SELECT id, autor from autores where id = 1
	UNION
	SELECT top 50 id, autor FROM autores
	order by 1 asc;

END;
GO
/****** Object:  StoredProcedure [dbo].[SP_GETCOINCIDENTES]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GETCOINCIDENTES]
(
  @AUTOROMATERIAOEDITORIAL VARCHAR(500),
  @OPCION VARCHAR(20)
)
AS
BEGIN
	
	IF @OPCION = 'A'
  BEGIN
	SELECT top 50 id, autor FROM autores WHERE autor LIKE '%' + @AUTOROMATERIAOEDITORIAL + '%' ORDER BY 2 ASC;
  END
  ELSE IF @OPCION = 'E'
  BEGIN
    
	 SELECT top 50 id, editorial from editoriales where editorial LIKE '%' + @AUTOROMATERIAOEDITORIAL + '%' ORDER BY 2 ASC;

  END
  ELSE IF @OPCION = 'M'
  BEGIN
    
	SELECT top 50 id, materia from materias where materia LIKE '%' + @AUTOROMATERIAOEDITORIAL + '%' ORDER BY 2 ASC;

  END
  ELSE IF @OPCION = 'S'
  BEGIN
    
	SELECT top 50 id, serie from series where serie LIKE '%' + @AUTOROMATERIAOEDITORIAL + '%' ORDER BY 2 ASC;

  END

END;
GO
/****** Object:  StoredProcedure [dbo].[SP_GETEDITORIALES]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GETEDITORIALES]
AS
BEGIN
	SELECT id, editorial from editoriales where id = 1
	UNION
	SELECT TOP 50
	id, editorial FROM editoriales order by 1 asc;

END;
GO
/****** Object:  StoredProcedure [dbo].[SP_GETEDITORIALESXBLOQUEDESCRIPCION]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GETEDITORIALESXBLOQUEDESCRIPCION]
    @Descripciones DescripcionTableType READONLY
AS
BEGIN
    SELECT e.id id,
	       e.editorial editorial
    FROM editoriales e
    WHERE editorial IN (SELECT Descripcion FROM @Descripciones);
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_GETIDLIBROXCODIGO]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GETIDLIBROXCODIGO]
(
  @CODIGO INT,
  @ID INT OUT
)
AS
BEGIN
	DECLARE @Result int;
	SET @Result = 0;
    SELECT @Result = id FROM libros WHERE codigo = @CODIGO;
    SET @ID = @Result;
  
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_GETIDXNOMBRE]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GETIDXNOMBRE]
(
  @AUTOROMATERIAOEDITORIAL VARCHAR(500),
  @OPCION VARCHAR(20),
  @ID INT OUT
)
AS
BEGIN
  IF @OPCION = 'A'
  BEGIN
    DECLARE @ResultA INT;
	SET @ResultA = 0;
	SET @ResultA = ( SELECT TOP 1 ID FROM autores WHERE autor = @AUTOROMATERIAOEDITORIAL);

    SET @ID = @ResultA;
  END
  ELSE IF @OPCION = 'E'
  BEGIN
    DECLARE @ResultE INT;
	SET @ResultE = ( SELECT TOP 1 ID FROM editoriales WHERE editorial = @AUTOROMATERIAOEDITORIAL);

    SET @ID = @ResultE;
  END
  ELSE IF @OPCION = 'M'
  BEGIN
    DECLARE @ResultM INT;
	SET @ResultM = ( SELECT TOP 1 ID FROM materias WHERE materia = @AUTOROMATERIAOEDITORIAL);

    SET @ID = @ResultM;
  END
  ELSE IF @OPCION = 'S'
  BEGIN
    DECLARE @ResultS INT;
	SET @ResultS = ( SELECT TOP 1 id FROM series WHERE serie = @AUTOROMATERIAOEDITORIAL);

    SET @ID = @ResultS;
  END
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_GETLIBROSDERESERVASPARAASIGNAR]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GETLIBROSDERESERVASPARAASIGNAR]
(
  @TITULO VARCHAR(500) = '-',
  @ID_SERIE INT = -1,
  @ID_AUTOR INT = -1,
  @ID_EDITORIAL INT = -1,
  @ID_MATERIA INT = -1,
  @CODIGO INT = -1,
  @ID_CLIENTE INT = -1,
  @RESERVA INT = -1
)
AS
BEGIN
	if @CODIGO > 0
	BEGIN
		SELECT
		r.id id_reserva,
		c.id id_cliente,
		c.nombre nom_cliente,
		l.id id_libro,
		l.codigo,
		l.isbn,
		l.ean13,
		l.titulo,
		l.id_autor,
		a.autor,
		l.id_serie,
		s.serie,
		l.id_editorial,
		e.editorial,
		l.id_materia,
		m.materia,
		l.precioLista,
		l.precio,
		l.contado,
		l.existencia,
		l.novedad,
		l.disponible,
		r.fchreserva,
		r.fchentrega,
		r.entregada
	FROM reservas r 
	join clientes c on r.id_cliente = c.id
	join libros l on r.id_libro = l.id
	JOIN series s ON l.id_serie = s.id
	JOIN autores a ON l.id_autor = a.id
	JOIN editoriales e ON l.id_editorial = e.id
	JOIN materias m ON l.id_materia = m.id
	WHERE 
		(CAST(l.codigo AS VARCHAR) LIKE '%' + CAST(@CODIGO AS VARCHAR) + '%') AND
		(@ID_CLIENTE = -1 OR c.id = @ID_CLIENTE) AND
		(r.asignada = 0) AND
		(r.entregada = 0) AND
		(r.activa = 1) AND
		(l.disponible = 1)
		order by 1 desc;
	END
	ELSE
		IF @RESERVA > 0
		BEGIN

			SELECT
			r.id id_reserva,
			c.id id_cliente,
			c.nombre nom_cliente,
			l.id id_libro,
			l.codigo,
			l.isbn,
			l.ean13,
			l.titulo,
			l.id_autor,
			a.autor,
			l.id_serie,
			s.serie,
			l.id_editorial,
			e.editorial,
			l.id_materia,
			m.materia,
			l.precioLista,
			l.precio,
			l.contado,
			l.existencia,
			l.novedad,
			l.disponible,
			r.fchreserva,
			r.fchentrega,
			r.entregada
		FROM reservas r 
		join clientes c on r.id_cliente = c.id
		join libros l on r.id_libro = l.id
		JOIN series s ON l.id_serie = s.id
		JOIN autores a ON l.id_autor = a.id
		JOIN editoriales e ON l.id_editorial = e.id
		JOIN materias m ON l.id_materia = m.id
		WHERE 
			--(CAST(r.id AS VARCHAR) LIKE '%' + CAST(@RESERVA AS VARCHAR) + '%') AND
			r.id = @RESERVA AND
			(@ID_CLIENTE = -1 OR c.id = @ID_CLIENTE) AND
			(r.asignada = 0) AND
			(r.entregada = 0) AND
			(r.activa = 1) AND
			(l.disponible = 1)
			order by 1 desc;
		END
		ELSE
			BEGIN
			SELECT TOP 200
			r.id id_reserva,
			c.id id_cliente,
			c.nombre nom_cliente,
			l.id id_libro,
			l.codigo,
			l.isbn,
			l.ean13,
			l.titulo,
			l.id_autor,
			a.autor,
			l.id_serie,
			s.serie,
			l.id_editorial,
			e.editorial,
			l.id_materia,
			m.materia,
			l.precioLista,
			l.precio,
			l.contado,
			l.existencia,
			l.novedad,
			l.disponible,
			r.fchreserva,
			r.fchentrega,
			r.entregada
		FROM reservas r 
		join clientes c on r.id_cliente = c.id
		join libros l on r.id_libro = l.id
		JOIN series s ON l.id_serie = s.id
		JOIN autores a ON l.id_autor = a.id
		JOIN editoriales e ON l.id_editorial = e.id
		JOIN materias m ON l.id_materia = m.id
		WHERE 
			((@TITULO = '-' OR ((l.titulo LIKE '%' + @TITULO + '%') OR (l.isbn LIKE '%' + @TITULO + '%') OR (l.ean13 LIKE '%' + @TITULO + '%')))) AND
			(@ID_SERIE = -1 OR l.id_serie = @ID_SERIE) AND
			(@ID_AUTOR = -1 OR l.id_autor = @ID_AUTOR) AND
			(@ID_EDITORIAL = -1 OR l.id_editorial = @ID_EDITORIAL) AND
			(@ID_MATERIA = -1 OR l.id_materia = @ID_MATERIA) AND
			--(@CODIGO = -1 OR CAST(l.codigo AS VARCHAR) LIKE '%' + CAST(@CODIGO AS VARCHAR) + '%') AND
			(@ID_CLIENTE = -1 OR c.id = @ID_CLIENTE) AND
			(r.asignada = 0) AND
			(r.entregada = 0) AND
			(r.activa = 1) AND
			(l.disponible = 1)
			order by 1 desc;
			END
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_GETLIBROSDERESERVASPARACHECKEAR]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GETLIBROSDERESERVASPARACHECKEAR]
(
  @TITULO VARCHAR(500) = '-',
  @ID_SERIE INT = -1,
  @ID_AUTOR INT = -1,
  @ID_EDITORIAL INT = -1,
  @ID_MATERIA INT = -1,
  @CODIGO INT = -1,
  @ID_CLIENTE INT = -1,
  @RESERVA INT = -1,
  @PSINASIGNAR INT = -1,
  @PPEDIDA INT = -1,
  @PASIGNADA INT = -1,
  @PENTREGADA INT = -1
)
AS
BEGIN
	if @CODIGO > 0
	BEGIN
		SELECT
		r.id id_reserva,
		c.id id_cliente,
		c.nombre nom_cliente,
		l.id id_libro,
		l.codigo,
		l.isbn,
		l.ean13,
		l.titulo,
		l.id_autor,
		a.autor,
		l.id_serie,
		s.serie,
		l.id_editorial,
		e.editorial,
		l.id_materia,
		m.materia,
		l.precioLista,
		l.precio,
		l.contado,
		l.existencia,
		l.novedad,
		l.disponible,
		r.fchreserva,
		r.fchentrega,
		r.entregada entregada,
		r.senia,
		r.saldo,
		r.libroPedido pedida,
		r.asignada asignada
	FROM reservas r 
	join clientes c on r.id_cliente = c.id
	join libros l on r.id_libro = l.id
	JOIN series s ON l.id_serie = s.id
	JOIN autores a ON l.id_autor = a.id
	JOIN editoriales e ON l.id_editorial = e.id
	JOIN materias m ON l.id_materia = m.id
	WHERE 
		(CAST(l.codigo AS VARCHAR) LIKE '%' + CAST(@CODIGO AS VARCHAR) + '%') AND
		(@ID_CLIENTE = -1 OR c.id = @ID_CLIENTE) AND
		(@PPEDIDA = -1 OR r.libroPedido = 1) AND
		(@PSINASIGNAR = -1 OR r.asignada = 0) AND
		(@PASIGNADA = -1 OR r.asignada = 1) AND
		(@PENTREGADA = -1 OR r.entregada = 1) AND
		(r.activa = 1) AND
		(l.disponible = 1)
		order by 1 desc;
	END
	ELSE
		IF @RESERVA > 0
		BEGIN

			SELECT
			r.id id_reserva,
			c.id id_cliente,
			c.nombre nom_cliente,
			l.id id_libro,
			l.codigo,
			l.isbn,
			l.ean13,
			l.titulo,
			l.id_autor,
			a.autor,
			l.id_serie,
			s.serie,
			l.id_editorial,
			e.editorial,
			l.id_materia,
			m.materia,
			l.precioLista,
			l.precio,
			l.contado,
			l.existencia,
			l.novedad,
			l.disponible,
			r.fchreserva,
			r.fchentrega,
			r.entregada entregada,
			r.senia,
			r.saldo,
			r.libroPedido pedida,
			r.asignada asignada
		FROM reservas r 
		join clientes c on r.id_cliente = c.id
		join libros l on r.id_libro = l.id
		JOIN series s ON l.id_serie = s.id
		JOIN autores a ON l.id_autor = a.id
		JOIN editoriales e ON l.id_editorial = e.id
		JOIN materias m ON l.id_materia = m.id
		WHERE 
			--(CAST(r.id AS VARCHAR) LIKE '%' + CAST(@RESERVA AS VARCHAR) + '%') AND
			r.id = @RESERVA AND
			(@ID_CLIENTE = -1 OR c.id = @ID_CLIENTE) AND
			(@PPEDIDA = -1 OR r.libroPedido = 1) AND
			(@PSINASIGNAR = -1 OR r.asignada = 0) AND
			(@PASIGNADA = -1 OR r.asignada = 1) AND
			(@PENTREGADA = -1 OR r.entregada = 1) AND
			(r.activa = 1) AND
			(l.disponible = 1)
			order by 1 desc;
		END
		ELSE
			BEGIN
			SELECT TOP 200
			r.id id_reserva,
			c.id id_cliente,
			c.nombre nom_cliente,
			l.id id_libro,
			l.codigo,
			l.isbn,
			l.ean13,
			l.titulo,
			l.id_autor,
			a.autor,
			l.id_serie,
			s.serie,
			l.id_editorial,
			e.editorial,
			l.id_materia,
			m.materia,
			l.precioLista,
			l.precio,
			l.contado,
			l.existencia,
			l.novedad,
			l.disponible,
			r.fchreserva,
			r.fchentrega,
			r.entregada entregada,
			r.senia,
			r.saldo,
			r.libroPedido pedida,
			r.asignada asignada
		FROM reservas r 
		join clientes c on r.id_cliente = c.id
		join libros l on r.id_libro = l.id
		JOIN series s ON l.id_serie = s.id
		JOIN autores a ON l.id_autor = a.id
		JOIN editoriales e ON l.id_editorial = e.id
		JOIN materias m ON l.id_materia = m.id
		WHERE 
			((@TITULO = '-' OR ((l.titulo LIKE '%' + @TITULO + '%') OR (l.isbn LIKE '%' + @TITULO + '%') OR (l.ean13 LIKE '%' + @TITULO + '%')))) AND
			(@ID_SERIE = -1 OR l.id_serie = @ID_SERIE) AND
			(@ID_AUTOR = -1 OR l.id_autor = @ID_AUTOR) AND
			(@ID_EDITORIAL = -1 OR l.id_editorial = @ID_EDITORIAL) AND
			(@ID_MATERIA = -1 OR l.id_materia = @ID_MATERIA) AND
			(@CODIGO = -1 OR CAST(l.codigo AS VARCHAR) LIKE '%' + CAST(@CODIGO AS VARCHAR) + '%') AND
			(@ID_CLIENTE = -1 OR c.id = @ID_CLIENTE) AND
			(@PPEDIDA = -1 OR r.libroPedido = 1) AND
			(@PSINASIGNAR = -1 OR r.asignada = 0) AND
			(@PASIGNADA = -1 OR r.asignada = 1) AND
			(@PENTREGADA = -1 OR r.entregada = 1) AND
			(r.activa = 1) AND
			(l.disponible = 1)
			order by 1 desc;
			END
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_GETLIBROSDERESERVASPARAENTREGAR]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GETLIBROSDERESERVASPARAENTREGAR]
(
  @TITULO VARCHAR(500) = '-',
  @ID_SERIE INT = -1,
  @ID_AUTOR INT = -1,
  @ID_EDITORIAL INT = -1,
  @ID_MATERIA INT = -1,
  @CODIGO INT = -1,
  @ID_CLIENTE INT = -1,
  @RESERVA INT = -1
)
AS
BEGIN
	if @CODIGO > 0
	BEGIN
		SELECT
		r.id id_reserva,
		c.id id_cliente,
		c.nombre nom_cliente,
		l.id id_libro,
		l.codigo,
		l.isbn,
		l.ean13,
		l.titulo,
		l.id_autor,
		a.autor,
		l.id_serie,
		s.serie,
		l.id_editorial,
		e.editorial,
		l.id_materia,
		m.materia,
		l.precioLista,
		l.precio,
		l.contado,
		l.existencia,
		l.novedad,
		l.disponible,
		r.fchreserva,
		r.fchentrega,
		r.entregada,
		r.senia,
		r.saldo,
		c.telefono
	FROM reservas r 
	join clientes c on r.id_cliente = c.id
	join libros l on r.id_libro = l.id
	JOIN series s ON l.id_serie = s.id
	JOIN autores a ON l.id_autor = a.id
	JOIN editoriales e ON l.id_editorial = e.id
	JOIN materias m ON l.id_materia = m.id
	WHERE 
		(CAST(l.codigo AS VARCHAR) LIKE '%' + CAST(@CODIGO AS VARCHAR) + '%') AND
		(@ID_CLIENTE = -1 OR c.id = @ID_CLIENTE) AND
		--(r.asignada = 1) AND
		(r.entregada = 0) AND
		(r.activa = 1) AND
		(l.disponible = 1)
		order by 1 desc;
	END
	ELSE
		IF @RESERVA > 0
		BEGIN

			SELECT
			r.id id_reserva,
			c.id id_cliente,
			c.nombre nom_cliente,
			l.id id_libro,
			l.codigo,
			l.isbn,
			l.ean13,
			l.titulo,
			l.id_autor,
			a.autor,
			l.id_serie,
			s.serie,
			l.id_editorial,
			e.editorial,
			l.id_materia,
			m.materia,
			l.precioLista,
			l.precio,
			l.contado,
			l.existencia,
			l.novedad,
			l.disponible,
			r.fchreserva,
			r.fchentrega,
			r.entregada,
			r.senia,
			r.saldo,
			c.telefono
		FROM reservas r 
		join clientes c on r.id_cliente = c.id
		join libros l on r.id_libro = l.id
		JOIN series s ON l.id_serie = s.id
		JOIN autores a ON l.id_autor = a.id
		JOIN editoriales e ON l.id_editorial = e.id
		JOIN materias m ON l.id_materia = m.id
		WHERE 
			--(CAST(r.id AS VARCHAR) LIKE '%' + CAST(@RESERVA AS VARCHAR) + '%') AND
			r.id = @RESERVA AND
			(@ID_CLIENTE = -1 OR c.id = @ID_CLIENTE) AND
			--(r.asignada = 1) AND
			(r.entregada = 0) AND
			(r.activa = 1) AND
			(l.disponible = 1)
			order by 1 desc;
		END
		ELSE
			BEGIN
			SELECT TOP 200
			r.id id_reserva,
			c.id id_cliente,
			c.nombre nom_cliente,
			l.id id_libro,
			l.codigo,
			l.isbn,
			l.ean13,
			l.titulo,
			l.id_autor,
			a.autor,
			l.id_serie,
			s.serie,
			l.id_editorial,
			e.editorial,
			l.id_materia,
			m.materia,
			l.precioLista,
			l.precio,
			l.contado,
			l.existencia,
			l.novedad,
			l.disponible,
			r.fchreserva,
			r.fchentrega,
			r.entregada,
			r.senia,
			r.saldo,
			c.telefono
		FROM reservas r 
		join clientes c on r.id_cliente = c.id
		join libros l on r.id_libro = l.id
		JOIN series s ON l.id_serie = s.id
		JOIN autores a ON l.id_autor = a.id
		JOIN editoriales e ON l.id_editorial = e.id
		JOIN materias m ON l.id_materia = m.id
		WHERE 
			((@TITULO = '-' OR ((l.titulo LIKE '%' + @TITULO + '%') OR (l.isbn LIKE '%' + @TITULO + '%') OR (l.ean13 LIKE '%' + @TITULO + '%')))) AND
			(@ID_SERIE = -1 OR l.id_serie = @ID_SERIE) AND
			(@ID_AUTOR = -1 OR l.id_autor = @ID_AUTOR) AND
			(@ID_EDITORIAL = -1 OR l.id_editorial = @ID_EDITORIAL) AND
			(@ID_MATERIA = -1 OR l.id_materia = @ID_MATERIA) AND
			--(@CODIGO = -1 OR CAST(l.codigo AS VARCHAR) LIKE '%' + CAST(@CODIGO AS VARCHAR) + '%') AND
			(@ID_CLIENTE = -1 OR c.id = @ID_CLIENTE) AND
			--(r.asignada = 1) AND
			(r.entregada = 0) AND
			(r.activa = 1) AND
			(l.disponible = 1)
			order by 1 desc
			;
			END
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_GETLIBROSPARAPEDIR]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GETLIBROSPARAPEDIR]
(
  @TITULO VARCHAR(500) = '-',
  @ID_SERIE INT = -1,
  @ID_AUTOR INT = -1,
  @ID_EDITORIAL INT = -1,
  @ID_MATERIA INT = -1,
  @CODIGO INT = -1
)
AS
BEGIN
if @CODIGO > 0
	BEGIN
		SELECT
		l.id id_libro,
		l.codigo,
		l.titulo,
		l.id_autor,
		a.autor,
		l.id_serie,
		s.serie,
		l.id_editorial,
		e.editorial,
		l.id_materia,
		m.materia,
		count(*) cantidad,
		STRING_AGG(r.id, ',') AS ids_reservas
	FROM reservas r 
	join clientes c on r.id_cliente = c.id
	join libros l on r.id_libro = l.id
	JOIN series s ON l.id_serie = s.id
	JOIN autores a ON l.id_autor = a.id
	JOIN editoriales e ON l.id_editorial = e.id
	JOIN materias m ON l.id_materia = m.id
	WHERE 
		(CAST(l.codigo AS VARCHAR) LIKE '%' + CAST(@CODIGO AS VARCHAR) + '%') AND
		((r.libroPedido = 0 AND r.asignada = 0)) AND -- OR (r.libroPedido = 1 AND r.asignada = 0)) AND
		(r.entregada = 0) AND
		(r.activa = 1) AND
		(l.disponible = 1)
	GROUP BY
		l.id, l.codigo, l.titulo,
		l.id_serie, s.serie, l.id_autor,
		a.autor, l.id_editorial, e.editorial,
		l.id_materia, m.materia
		order by 1 desc
		;
	END
	ELSE
	BEGIN
		SELECT TOP 200
		l.id id_libro,
		l.codigo,
		l.titulo,
		l.id_autor,
		a.autor,
		l.id_serie,
		s.serie,
		l.id_editorial,
		e.editorial,
		l.id_materia,
		m.materia,
		count(*) cantidad,
		STRING_AGG(r.id, ',') AS ids_reservas
	FROM reservas r 
	join clientes c on r.id_cliente = c.id
	join libros l on r.id_libro = l.id
	JOIN series s ON l.id_serie = s.id
	JOIN autores a ON l.id_autor = a.id
	JOIN editoriales e ON l.id_editorial = e.id
	JOIN materias m ON l.id_materia = m.id
	WHERE 
		((@TITULO = '-' OR ((l.titulo LIKE '%' + @TITULO + '%') OR (l.isbn LIKE '%' + @TITULO + '%') OR (l.ean13 LIKE '%' + @TITULO + '%')))) AND
		(@ID_SERIE = -1 OR l.id_serie = @ID_SERIE) AND
		(@ID_AUTOR = -1 OR l.id_autor = @ID_AUTOR) AND
		(@ID_EDITORIAL = -1 OR l.id_editorial = @ID_EDITORIAL) AND
		(@ID_MATERIA = -1 OR l.id_materia = @ID_MATERIA) AND
		(@CODIGO = -1 OR CAST(l.codigo AS VARCHAR) LIKE '%' + CAST(@CODIGO AS VARCHAR) + '%') AND
		((r.libroPedido = 0 AND r.asignada = 0) ) AND--OR (r.libroPedido = 1 AND r.asignada = 0)) AND
		(r.entregada = 0) AND
		(r.activa = 1) AND
		(l.disponible = 1)
	GROUP BY
		l.id, l.codigo, l.titulo,
		l.id_serie, s.serie, l.id_autor,
		a.autor, l.id_editorial, e.editorial,
		l.id_materia, m.materia
		order by 1 desc
		;
		END
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GETLIBROSXBLOQUECODIGO]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GETLIBROSXBLOQUECODIGO]
    @Codigos CodigoTableType READONLY
AS
BEGIN
    SELECT l.id id,
           codigo,
           isbn,
           ean13,
           titulo,
           id_autor,
           autor,
           id_serie,
           serie,
           id_editorial,
           editorial,
           id_materia,
           materia,
           precioLista,
           precio,
           contado,
           existencia,
           novedad,
           disponible,
		   cargadoUltimaVezPorExcel
    FROM libros l
    JOIN series s ON l.id_serie = s.id
    JOIN materias m ON l.id_materia = m.id
    JOIN editoriales e ON l.id_editorial = e.id
    JOIN autores a ON l.id_autor = a.id
    WHERE codigo IN (SELECT Codigo FROM @Codigos);
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_GETLIBROSXTITULOAUTOREDITORIALYOCODIGO]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GETLIBROSXTITULOAUTOREDITORIALYOCODIGO]
(
  @TITULO VARCHAR(500) = '-',
  @ID_SERIE INT = -1,
  @ID_AUTOR INT = -1,
  @ID_EDITORIAL INT = -1,
  @ID_MATERIA INT = -1,
  @CODIGO INT = -1
)
AS
BEGIN
	if @CODIGO > 0
	BEGIN
		SELECT TOP 200
		l.id,
		l.codigo,
		l.isbn,
		l.ean13,
		l.titulo,
		l.id_autor,
		a.autor,
		l.id_serie,
		s.serie,
		l.id_editorial,
		e.editorial,
		l.id_materia,
		m.materia,
		l.precioLista,
		l.precio,
		l.contado,
		l.existencia,
		l.novedad,
		l.disponible,
		l.fec_mod,
		l.cargadoUltimaVezPorExcel
	FROM libros l
	JOIN series s ON l.id_serie = s.id
	JOIN autores a ON l.id_autor = a.id
	JOIN editoriales e ON l.id_editorial = e.id
	JOIN materias m ON l.id_materia = m.id
	WHERE 
		CAST(l.codigo AS VARCHAR) LIKE '%' + CAST(@CODIGO AS VARCHAR) + '%' AND
		(l.disponible = 1)
		order by 1 desc;
	END
	ELSE
	BEGIN
	SELECT TOP 200
		l.id,
		l.codigo,
		l.isbn,
		l.ean13,
		l.titulo,
		l.id_autor,
		a.autor,
		l.id_serie,
		s.serie,
		l.id_editorial,
		e.editorial,
		l.id_materia,
		m.materia,
		l.precioLista,
		l.precio,
		l.contado,
		l.existencia,
		l.novedad,
		l.disponible,
		l.fec_mod,
		l.cargadoUltimaVezPorExcel
	FROM libros l
	JOIN series s ON l.id_serie = s.id
	JOIN autores a ON l.id_autor = a.id
	JOIN editoriales e ON l.id_editorial = e.id
	JOIN materias m ON l.id_materia = m.id
	WHERE 
		((@TITULO = '-' OR ((l.titulo LIKE '%' + @TITULO + '%') OR (l.isbn LIKE '%' + @TITULO + '%') OR (l.ean13 LIKE '%' + @TITULO + '%')))) AND
		(@ID_SERIE = -1 OR l.id_serie = @ID_SERIE) AND
		(@ID_AUTOR = -1 OR l.id_autor = @ID_AUTOR) AND
		(@ID_EDITORIAL = -1 OR l.id_editorial = @ID_EDITORIAL) AND
		(@ID_MATERIA = -1 OR l.id_materia = @ID_MATERIA) AND
		--(@CODIGO = -1 OR CAST(l.codigo AS VARCHAR) LIKE '%' + CAST(@CODIGO AS VARCHAR) + '%') AND
		(l.disponible = 1)
		order by 1 desc;
	END
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_GETLIBROXCODIGO]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GETLIBROXCODIGO]
	@CODIGO INT
AS
BEGIN

	SELECT l.id
		   , codigo
		   , isbn
		   , ean13
		   , titulo
		   , id_autor
		   , autor
		   , id_serie
		   , serie
		   , id_editorial
		   , editorial
		   , id_materia
		   , materia
		   , precioLista
		   , precio
		   , contado
		   , existencia
		   , novedad
		   , disponible
		   FROM libros l
		   JOIN series s on l.id_serie = s.id
		   JOIN materias m on l.id_materia = m.id
		   JOIN editoriales e on l.id_editorial = e.id
		   JOIN autores a on l.id_autor = a.id
		   WHERE codigo = @CODIGO;

END;
GO
/****** Object:  StoredProcedure [dbo].[SP_GETLIBROXID]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GETLIBROXID]
	@PIDLIBRO INT
AS
BEGIN

	SELECT l.id
		   , codigo
		   , isbn
		   , ean13
		   , titulo
		   , id_autor
		   , autor
		   , id_serie
		   , serie
		   , id_editorial
		   , editorial
		   , id_materia
		   , materia
		   , precioLista
		   , precio
		   , contado
		   , existencia
		   , novedad
		   , disponible
		   FROM libros l
		   JOIN series s on l.id_serie = s.id
		   JOIN materias m on l.id_materia = m.id
		   JOIN editoriales e on l.id_editorial = e.id
		   JOIN autores a on l.id_autor = a.id
		   WHERE l.id = @PIDLIBRO;

END;
GO
/****** Object:  StoredProcedure [dbo].[SP_GETMATERIAS]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GETMATERIAS]
AS
BEGIN
	SELECT id, materia from materias where id = 1
	UNION
	SELECT top 50
	id, materia FROM materias order by 1 asc;

END;
GO
/****** Object:  StoredProcedure [dbo].[SP_GETMATERIASXBLOQUEDESCRIPCION]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GETMATERIASXBLOQUEDESCRIPCION]
    @Descripciones DescripcionTableType READONLY
AS
BEGIN
    SELECT m.id id,
	       m.materia materia
    FROM materias m
    WHERE materia IN (SELECT Descripcion FROM @Descripciones);
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_GETMONTOSXRESERVA]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GETMONTOSXRESERVA]
(
  @PIDRESERVA INT
  , @PFORMAPAGO INT
)
AS
BEGIN
    DECLARE @formaPago INT;

    SELECT @formaPago = id_forma_pago
    FROM reservas
    WHERE id = @PIDRESERVA;

    IF @formaPago = 3
    BEGIN
        SELECT 
            r.id,
            id_libro,
            senia,
            saldo,
            l.precio AS precioLibro,
            l.contado AS contadoLibro,
			r.id_forma_pago formaPago
        FROM reservas r
        JOIN libros l ON r.id_libro = l.id
        WHERE r.id = @PIDRESERVA
        ORDER BY r.id ASC;
    END
    ELSE
    BEGIN
        SELECT 
            r.id,
            id_libro,
            senia,
            saldo,
            l.precio AS precioLibro,
            l.contado AS contadoLibro,
			r.id_forma_pago formaPago
        FROM reservas r
        JOIN libros l ON r.id_libro = l.id
        WHERE r.id = @PIDRESERVA
        ORDER BY r.id ASC;
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_GetPaymentsByMethodAndPeriod]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GetPaymentsByMethodAndPeriod]
    @PFormaPagoID INT = NULL,
    @PFechaInicio DATETIME = NULL,
    @PFechaFin DATETIME = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    IF @PFechaInicio IS NULL
        SET @PFechaInicio = DATEADD(DAY, -30, GETDATE());
        
    IF @PFechaFin IS NULL
        SET @PFechaFin = GETDATE();
    
    CREATE TABLE #ResultadosPagos (
        FormaPagoID INT,
        FormaPago NVARCHAR(200),
        LibroID INT,
        Titulo VARCHAR(300),
        ClienteID INT,
        NombreCliente NVARCHAR(150),
        FechaReserva DATETIME,
        PrecioLibro DECIMAL(10, 2),
        Senia MONEY,
        Saldo MONEY,
        MontoRecaudado DECIMAL(10, 2)
    );
    
    INSERT INTO #ResultadosPagos
    SELECT 
        fp.id AS FormaPagoID,
        fp.formapago AS FormaPago,
        l.id AS LibroID,
        l.titulo AS Titulo,
        c.id AS ClienteID,
        c.nombre AS NombreCliente,
        r.fchreserva AS FechaReserva,
        CASE 
            WHEN fp.formapago = 'Crédito' THEN
                ISNULL((
                    SELECT TOP 1 p.precio
                    FROM precios p
                    WHERE p.id_libro = l.id
                    AND p.fchDesde <= r.fchreserva
                    AND (p.fchHasta IS NULL OR p.fchHasta > r.fchreserva)
                    ORDER BY p.fchDesde DESC
                ), l.precio)
            ELSE
                ISNULL((
                    SELECT TOP 1 p.contado
                    FROM precios p
                    WHERE p.id_libro = l.id
                    AND p.fchDesde <= r.fchreserva
                    AND (p.fchHasta IS NULL OR p.fchHasta > r.fchreserva)
                    ORDER BY p.fchDesde DESC
                ), l.contado)
        END AS PrecioLibro,
        r.senia AS Senia,
        r.saldo AS Saldo,
        CASE 
            WHEN fp.formapago = 'Crédito' THEN
                ISNULL((
                    SELECT TOP 1 p.precio
                    FROM precios p
                    WHERE p.id_libro = l.id
                    AND p.fchDesde <= r.fchreserva
                    AND (p.fchHasta IS NULL OR p.fchHasta > r.fchreserva)
                    ORDER BY p.fchDesde DESC
                ), l.precio)
            ELSE
                ISNULL((
                    SELECT TOP 1 p.contado
                    FROM precios p
                    WHERE p.id_libro = l.id
                    AND p.fchDesde <= r.fchreserva
                    AND (p.fchHasta IS NULL OR p.fchHasta > r.fchreserva)
                    ORDER BY p.fchDesde DESC
                ), l.contado)
        END - CONVERT(DECIMAL(10,2), r.saldo) AS MontoRecaudado
    FROM 
        reservas r
        JOIN libros l ON r.id_libro = l.id
        JOIN clientes c ON r.id_cliente = c.id
        JOIN formaspago fp ON r.id_forma_pago = fp.id
    WHERE 
        r.fchreserva BETWEEN @PFechaInicio AND @PFechaFin
        AND (@PFormaPagoID IS NULL OR r.id_forma_pago = @PFormaPagoID);
    
    SELECT 
        FormaPagoID,
        FormaPago,
        COUNT(LibroID) AS CantidadReservas,
        SUM(MontoRecaudado) AS TotalRecaudado
    FROM 
        #ResultadosPagos
    GROUP BY 
        FormaPagoID, FormaPago
    ORDER BY 
        FormaPago;
    
    SELECT 
        FormaPagoID,
        FormaPago,
        LibroID,
        Titulo,
        ClienteID,
        NombreCliente,
        FechaReserva,
        PrecioLibro,
        Senia,
        Saldo,
        MontoRecaudado
    FROM 
        #ResultadosPagos
    ORDER BY 
        FormaPago, FechaReserva DESC;
    
    DROP TABLE #ResultadosPagos;
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_GETPRECIOLIBROXFECHAORESERVA]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GETPRECIOLIBROXFECHAORESERVA]
    @PIDLIBRO INT,
    @PIDRESERVA INT,
    @PFECHA DATETIME
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @precioReserva DECIMAL(10, 2);
    DECLARE @precio DECIMAL(10, 2);
    DECLARE @precioLista DECIMAL(10, 2);
    DECLARE @contado DECIMAL(10, 2);

    SELECT TOP 1 @precioLista = precioLista,
				 @contado = contado,
				 @precio = precio
    FROM precios
    WHERE id_libro = @PIDLIBRO
      AND fchDesde <= @PFECHA
      AND (fchHasta IS NULL OR fchHasta > @PFECHA)
    ORDER BY fchDesde DESC;

    -- Si no se encontró precio en la tabla de precios, buscar el precio en la reserva
    IF @precioReserva IS NULL
    BEGIN
        SELECT @precioReserva = ISNULL(saldo + senia, 0)
        FROM reservas
        WHERE id = @PIDRESERVA;
    END;

	SELECT l.id id
		   , codigo
		   , isbn
		   , ean13
		   , titulo
		   , id_autor
		   , autor
		   , id_serie
		   , serie
		   , id_editorial
		   , editorial
		   , id_materia
		   , materia
		   , ISNULL(@precioLista, precioLista) as precioLista
		   , ISNULL(@precio, precio) as precio
		   , ISNULL(@contado, contado) as contado
		   , existencia
		   , novedad
		   , disponible
		   , ISNULL(@precioReserva, 0) AS precioFinal
		   FROM libros l
		   JOIN series s on l.id_serie = s.id
		   JOIN materias m on l.id_materia = m.id
		   JOIN editoriales e on l.id_editorial = e.id
		   JOIN autores a on l.id_autor = a.id
		   WHERE l.id = @PIDLIBRO;
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_GETPROVEEDORES]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GETPROVEEDORES]
AS
BEGIN

	SELECT 
		id,
		proveedor,
		direccion,
		id_localidad,
		cod_postal,
		telefono,
		nro_iva
	FROM proveedores
	order by id asc;

END
GO
/****** Object:  StoredProcedure [dbo].[SP_GETRESERVAXID]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GETRESERVAXID]
    @PIDRESERVA INT
AS
BEGIN
    SET NOCOUNT ON;

	SELECT r.id
		 , r.id_libro
		 , r.id_cliente
		 , r.id_proveedor
		 , r.senia
		 , r.saldo
		 , r.fchreserva
		 , r.fchentrega
		 , r.id_forma_pago
		 , r.libroPedido
		 , r.asignada
		 , r.entregada
		 , r.activa
		 from reservas r
		 where r.id = @PIDRESERVA;
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_GETROLES]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GETROLES]
AS
BEGIN
    SELECT id, nombre, descripcion, estado 
    FROM roles 
    WHERE estado = 1
    ORDER BY nombre;
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_GETSERIES]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GETSERIES]
AS
BEGIN
	SELECT id, serie from series where id = 1
	UNION
	SELECT top 50
	id, serie FROM series order by 1 asc;

END;
GO
/****** Object:  StoredProcedure [dbo].[SP_GETSERIESXBLOQUEDESCRIPCION]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GETSERIESXBLOQUEDESCRIPCION]
    @Descripciones DescripcionTableType READONLY
AS
BEGIN
    SELECT s.id id,
	       s.serie serie
    FROM series s
    WHERE serie IN (SELECT Descripcion FROM @Descripciones);
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_GETUSER]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GETUSER]
    @PIDUSUARIO INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        u.id,
        u.nombre,
        u.apellido,
        u.usuario AS usuario,
        u.pass AS pass,
        u.activo,
        u.fechaMod,
        u.idRol,
        r.nombre AS rol,
        r.descripcion,
        r.estado
    FROM usuarios u
    INNER JOIN roles r ON r.id = u.idRol
    WHERE u.id = @PIDUSUARIO;
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_GETUSUARIOS]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GETUSUARIOS]
AS
BEGIN
    SELECT 
        u.id,
        u.nombre,
        u.apellido,
        u.usuario,
        u.pass,
        u.activo,
        u.fechaMod,
        u.idRol,
        r.nombre as rol,
        r.descripcion,
        r.estado
    FROM usuarios u
    INNER JOIN roles r ON r.id = u.idRol
    ORDER BY u.id DESC;
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_GUARDAR]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GUARDAR]
(
  @PID INT OUTPUT,
  @AUTOROMATERIAOEDITORIAL VARCHAR(500),
  @OPCION VARCHAR(20)
)
AS
BEGIN
  IF @OPCION = 'A'
  BEGIN
    INSERT INTO autores (autor)
	VALUES (@AUTOROMATERIAOEDITORIAL);
	SET @PID = SCOPE_IDENTITY();
  END
  ELSE IF @OPCION = 'E'
  BEGIN
    INSERT INTO editoriales (editorial)
	VALUES (@AUTOROMATERIAOEDITORIAL);
	SET @PID = SCOPE_IDENTITY();
  END
  ELSE IF @OPCION = 'M'
  BEGIN
    INSERT INTO materias (materia)
	VALUES (@AUTOROMATERIAOEDITORIAL);
	SET @PID = SCOPE_IDENTITY();
  END
  ELSE IF @OPCION = 'S'
  BEGIN
    INSERT INTO series (serie)
	VALUES (@AUTOROMATERIAOEDITORIAL);
	SET @PID = SCOPE_IDENTITY();
  END
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_GUARDARCLIENTE]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GUARDARCLIENTE]
(
  @PCODIGO INT OUTPUT,
  @PNOMBRE VARCHAR(20),
  @PTELEFONO VARCHAR(20),
  @PDIRECCION VARCHAR(200),
  @PIDLOCALIDAD INT,
  @PEMAIL VARCHAR(200)
)
AS
BEGIN
	SELECT @PCODIGO = ISNULL(MAX(codigo), 0) + 1 FROM clientes;
	INSERT INTO clientes
	(
		codigo, nombre, telefono,
		direccion, email, id_localidad
	)
	VALUES 
	(
		@PCODIGO, @PNOMBRE, @PTELEFONO,
		@PDIRECCION, @PEMAIL, @PIDLOCALIDAD
	);
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_GUARDARLIBRO]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GUARDARLIBRO]
(
  @ID INT,
  @CODIGO INT,
  @ISBN VARCHAR(20),
  @EAN13 VARCHAR(20),
  @TITULO VARCHAR(200),
  @ID_AUTOR INT,
  @ID_SERIE INT,
  @ID_EDITORIAL INT,
  @ID_MATERIA INT,
  @PRECIOLISTA DECIMAL(10, 2),
  @PRECIO DECIMAL(10, 2),
  @CONTADO DECIMAL(10, 2),
  @EXISTENCIA INT,
  @NOVEDAD BIT,
  @DISPONIBLE BIT,
  @CARGADOULTVEZPOREXCEL BIT
)
AS
BEGIN
  INSERT INTO libros 
  (
    codigo, isbn, ean13, titulo, id_autor, id_serie,
    id_editorial, id_materia, preciolista, precio,
    contado, existencia, novedad, disponible, fec_mod, cargadoUltimaVezPorExcel
  )
  VALUES 
  (
    @CODIGO, @ISBN, @EAN13, @TITULO, @ID_AUTOR, @ID_SERIE,
    @ID_EDITORIAL, @ID_MATERIA, @PRECIOLISTA, @PRECIO,
    @CONTADO, @EXISTENCIA, @NOVEDAD, @DISPONIBLE, SYSDATETIME(), @CARGADOULTVEZPOREXCEL
  );
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_GUARDARPEDIDO]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GUARDARPEDIDO]
(
  @PIDPEDIDO INT OUTPUT,
  @PIDLIBRO INT,
  @PIDPROVEEDOR INT,
  @PCANTIDAD INT,
  @PFCHPEDIDO DATETIME,
  @IDSRESERVAS VARCHAR(5000)
)
AS
BEGIN
  INSERT INTO pedidos
  (
    id_libro, id_proveedor,
    cantidad, fchpedido
  )
  VALUES
  (
    @PIDLIBRO, @PIDPROVEEDOR,
    @PCANTIDAD, @PFCHPEDIDO
  );
  
  
  UPDATE reservas
  SET libroPedido = 1,
	id_proveedor = @PIDPROVEEDOR
  WHERE id IN (
    SELECT value
    FROM STRING_SPLIT(@IDSRESERVAS, ',')
  );
  SET @PIDPEDIDO = SCOPE_IDENTITY();
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GUARDARPEDIDODOS]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GUARDARPEDIDODOS]
(
  @PIDPEDIDO INT OUTPUT,
  @PIDLIBRO INT,
  @PIDPROVEEDOR INT,
  @PCANTIDAD INT,
  @PFCHPEDIDO DATETIME,
  @IDSRESERVAS VARCHAR(5000)
)
AS
BEGIN
  BEGIN TRY
    BEGIN TRANSACTION;

    INSERT INTO pedidos
    (
      id_libro, id_proveedor,
      cantidad, fchpedido
    )
    VALUES
    (
      @PIDLIBRO, @PIDPROVEEDOR,
      @PCANTIDAD, @PFCHPEDIDO
    );

    SET @PIDPEDIDO = SCOPE_IDENTITY();

    IF @PIDPEDIDO IS NULL OR @PIDPEDIDO = 0
    BEGIN
      THROW 50000, 'No se pudo obtener el ID del pedido recién creado.', 1;
    END

    UPDATE reservas
    SET libroPedido = 1
    WHERE id IN (
      SELECT value
      FROM STRING_SPLIT(@IDSRESERVAS, ',')
    );

    COMMIT TRANSACTION;
  END TRY
  BEGIN CATCH
    ROLLBACK TRANSACTION;

    DECLARE @ErrorMessage NVARCHAR(4000);
    DECLARE @ErrorSeverity INT;
    DECLARE @ErrorState INT;

    SELECT 
      @ErrorMessage = ERROR_MESSAGE(),
      @ErrorSeverity = ERROR_SEVERITY(),
      @ErrorState = ERROR_STATE();

    RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);

    SET @PIDPEDIDO = -1;
  END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[SP_GUARDARRESERVA]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GUARDARRESERVA]
(
  @PIDRESERVA INT OUTPUT,
  @PIDLIBRO INT,
  @PIDCLIENTE INT,
  @PIDPROVEEDOR INT,
  @PSEÑA DECIMAL(10, 2),
  @PSALDO DECIMAL(10, 2),
  @PFCHRESERVA DATETIME,
  @PIDFORMAPAGO INT,
  @PLIBROPEDIDO BIT,
  @PASIGNADA BIT,
  @PENTREGADA BIT,
  @PACTIVA BIT
)
AS
BEGIN
  INSERT INTO reservas
  (
    id_libro, id_cliente, id_proveedor,
    senia, saldo, fchreserva, id_forma_pago,
	libroPedido, entregada, asignada, activa
  )
  VALUES
  (
    @PIDLIBRO, @PIDCLIENTE, @PIDPROVEEDOR,
    @PSEÑA, @PSALDO, @PFCHRESERVA, @PIDFORMAPAGO,
	@PLIBROPEDIDO, @PENTREGADA, @PASIGNADA, @PACTIVA
  );

  SET @PIDRESERVA = SCOPE_IDENTITY();
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_GUARDARUSUARIO]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_GUARDARUSUARIO]
(
    @PIDUSUARIO INT OUTPUT,
    @PNOMBRE NVARCHAR(100),
    @PAPELLIDO NVARCHAR(100),
    @PUSUARIO NVARCHAR(100),
    @PPASS NVARCHAR(100),
    @PACTIVO BIT,
    @PIDROL INT
)
AS
BEGIN
    INSERT INTO usuarios 
    (
        nombre, apellido, usuario, pass, 
        activo, fechaMod, idRol
    )
    VALUES 
    (
        @PNOMBRE, @PAPELLIDO, @PUSUARIO, @PPASS,
        @PACTIVO, GETDATE(), @PIDROL
    );
    
    SET @PIDUSUARIO = SCOPE_IDENTITY();
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_LOGIN]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_LOGIN]
    @PUSUARIO NVARCHAR(100),
    @PCONTRASENIA NVARCHAR(100),
    @PLOGUEADO INT OUTPUT,
    @PIDUSER INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ID INT = NULL;
    DECLARE @ACTIVO BIT = NULL;
    DECLARE @PASS NVARCHAR(100) = NULL;

    SELECT @ID = id, @PASS = pass, @ACTIVO = activo
    FROM usuarios
    WHERE usuario = @PUSUARIO;

    IF @ID IS NULL
    BEGIN
        SET @PLOGUEADO = -2;  -- Usuario no existe
        SET @PIDUSER = NULL;
    END
    ELSE IF @ACTIVO = 0 OR @ACTIVO IS NULL
    BEGIN
        SET @PLOGUEADO = -1;  -- Usuario inactivo
        SET @PIDUSER = NULL;
    END
    ELSE IF @PASS IS NULL OR @PASS <> @PCONTRASENIA
    BEGIN
        SET @PLOGUEADO = 0;  -- Contraseña incorrecta
        SET @PIDUSER = NULL;
    END
    ELSE
    BEGIN
        SET @PLOGUEADO = 1;  -- Login exitoso
        SET @PIDUSER = @ID;
    END
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_MODIFICARCLIENTE]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_MODIFICARCLIENTE]
(
  @PID INT,
  @PNOMBRE VARCHAR(20),
  @PTELEFONO VARCHAR(20),
  @PDIRECCION VARCHAR(200),
  @PIDLOCALIDAD INT,
  @PEMAIL VARCHAR(200)
)
AS
BEGIN

  UPDATE clientes
	SET
	nombre = @PNOMBRE,
	telefono = @PTELEFONO,
	direccion = @PDIRECCION,
	email = @PEMAIL,
	id_localidad = @PIDLOCALIDAD
    WHERE id = @PID
	;
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_MODIFICARLIBRO]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_MODIFICARLIBRO]
(
  @ID INT,
  @CODIGO INT,
  @ISBN VARCHAR(20),
  @EAN13 VARCHAR(20),
  @TITULO VARCHAR(200),
  @ID_AUTOR INT,
  @ID_SERIE INT,
  @ID_EDITORIAL INT,
  @ID_MATERIA INT,
  @PRECIOLISTA DECIMAL(10, 2),
  @PRECIO DECIMAL(10, 2),
  @CONTADO DECIMAL(10, 2),
  @EXISTENCIA INT,
  @NOVEDAD BIT,
  @DISPONIBLE BIT,
  @CARGADOULTVEZPOREXCEL BIT

)
AS
BEGIN
  BEGIN TRY
    UPDATE libros 
    SET  
      codigo = @CODIGO,
      isbn = @ISBN,
      ean13 = @EAN13,
      titulo = @TITULO,
      id_autor = @ID_AUTOR,
	  id_serie = @ID_SERIE,
      id_editorial = @ID_EDITORIAL,
      id_materia = @ID_MATERIA,
      preciolista = @PRECIOLISTA,
      precio = @PRECIO,
      contado = @CONTADO,
      existencia = @EXISTENCIA,
      novedad = @NOVEDAD,
	  disponible = @DISPONIBLE,
	  fec_mod = SYSDATETIME(),
	  cargadoUltimaVezPorExcel = @CARGADOULTVEZPOREXCEL
    WHERE id = @ID;
  END TRY
  BEGIN CATCH
    DECLARE @ErrorMessage NVARCHAR(4000);
    DECLARE @ErrorSeverity INT;
    DECLARE @ErrorState INT;

    SELECT 
      @ErrorMessage = ERROR_MESSAGE(),
      @ErrorSeverity = ERROR_SEVERITY(),
      @ErrorState = ERROR_STATE();

    RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
  END CATCH
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_MODIFICARPROVEDOR]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_MODIFICARPROVEDOR]
(
    @PID INT,
    @PNOMBRE VARCHAR(200),
    @PCONTACTO VARCHAR(200) = NULL,
    @PTELEFONO VARCHAR(50) = NULL,
    @PEMAIL VARCHAR(200) = NULL,
    @PDIRECCION VARCHAR(500) = NULL,
    @POBSERVACIONES VARCHAR(1000) = NULL,
    @PACTIVO BIT = 1
)
AS
BEGIN
    UPDATE proveedores
    SET
        proveedor = @PNOMBRE,
        contacto = @PCONTACTO,
        telefono = @PTELEFONO,
        email = @PEMAIL,
        direccion = @PDIRECCION,
        observaciones = @POBSERVACIONES,
        activo = @PACTIVO
    WHERE id = @PID;
    
    SELECT @@ROWCOUNT as FilasAfectadas;
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_MODIFICARUSUARIO]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_MODIFICARUSUARIO]
(
    @PID INT,
    @PNOMBRE NVARCHAR(100),
    @PAPELLIDO NVARCHAR(100),
    @PUSUARIO NVARCHAR(100),
    @PPASS NVARCHAR(100),
    @PACTIVO BIT,
    @PIDROL INT
)
AS
BEGIN
    UPDATE usuarios 
    SET 
        nombre = @PNOMBRE,
        apellido = @PAPELLIDO,
        usuario = @PUSUARIO,
        pass = @PPASS,
        activo = @PACTIVO,
        fechaMod = GETDATE(),
        idRol = @PIDROL
    WHERE id = @PID;
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_REGISTRARPAGO]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_REGISTRARPAGO]
(
    @PIDPAGO INT OUTPUT,
    @PMONTO DECIMAL(10,2),
    @PMETODO_PAGO NVARCHAR(50),
    @PESTADO_PAGO NVARCHAR(20),
    @PPREFERENCE_ID NVARCHAR(200) = NULL,
    @PPAYMENT_ID NVARCHAR(200) = NULL,
    @PEXTERNAL_REFERENCE NVARCHAR(500) = NULL,
    @PRESERVAS_IDS NVARCHAR(1000)
)
AS
BEGIN
    INSERT INTO pagos
    (
        monto, metodo_pago, estado_pago, fecha_pago,
        preference_id, payment_id, external_reference, 
        reservas_ids, activo
    )
    VALUES
    (
        @PMONTO, @PMETODO_PAGO, @PESTADO_PAGO, GETDATE(),
        @PPREFERENCE_ID, @PPAYMENT_ID, @PEXTERNAL_REFERENCE,
        @PRESERVAS_IDS, 1
    );
    
    SET @PIDPAGO = SCOPE_IDENTITY();
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_TRAERCLIENTES]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_TRAERCLIENTES]
AS
BEGIN
	SELECT top 50
			 c.id
		   , c.codigo
		   , c.nombre
		   , c.direccion
		   , c.id_localidad
		   , l.localidad
		   , c.telefono
		   , c.email
		   FROM clientes c
		   join localidades l on c.id_localidad = l.id
		   order by c.codigo desc;
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_TRAERCLIENTESXNOMBRE]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_TRAERCLIENTESXNOMBRE]
(
  @PNOMBRE VARCHAR(500) = '-'
)
AS
BEGIN
	SELECT TOP 200
			 c.id
		   , c.codigo
		   , c.nombre
		   , c.direccion
		   , c.id_localidad
		   , l.localidad
		   , c.telefono
		   , c.email
		   FROM clientes c
		   join localidades l on c.id_localidad = l.id
	WHERE 
		(@PNOMBRE = '-' OR c.nombre LIKE '%' + @PNOMBRE + '%' OR c.telefono LIKE '%' + @PNOMBRE + '%')
		   order by c.codigo desc;
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_TRAERCLIENTEXCODIGO]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_TRAERCLIENTEXCODIGO]
(
  @PCODIGO VARCHAR(50) = -1
)
AS
BEGIN
	if @PCODIGO > 0
	BEGIN
		SELECT
			 c.id
		   , c.codigo
		   , c.nombre
		   , c.direccion
		   , c.id_localidad
		   , l.localidad
		   , c.telefono
		   , c.email
		   FROM clientes c
		   join localidades l on c.id_localidad = l.id
	WHERE 
		CAST(c.codigo AS VARCHAR) = @PCODIGO
		   order by c.codigo desc;
	END
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_TRAERCLIENTEXID]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_TRAERCLIENTEXID]
(
  @PIDCLIENTE INT = -1
)
AS
BEGIN

	SELECT c.id
		   , c.codigo
		   , c.nombre
		   , c.direccion
		   , c.id_localidad
		   , l.localidad
		   , c.telefono
		   , c.email
		   FROM clientes c
		   join localidades l on c.id_localidad = l.id
		   WHERE c.id = @PIDCLIENTE;

END;
GO
/****** Object:  StoredProcedure [dbo].[SP_TRAERCLIENTEXTELEFONO]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_TRAERCLIENTEXTELEFONO]
	@PTELEFONO VARCHAR(30)
AS
BEGIN

	SELECT c.id
		   , c.codigo
		   , c.nombre
		   , c.direccion
		   , c.id_localidad
		   , l.localidad
		   , c.telefono
		   , c.email
		   FROM clientes c
		   join localidades l on c.id_localidad = l.id
		   WHERE telefono LIKE '%' + @PTELEFONO + '%';

END;
GO
/****** Object:  StoredProcedure [dbo].[SP_TRAERFORMASPAGO]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_TRAERFORMASPAGO]
AS
BEGIN

	SELECT id, formapago FROM formaspago;

END;
GO
/****** Object:  StoredProcedure [dbo].[SP_TRAERFORMASPAGO_CON_PORCENTAJE]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_TRAERFORMASPAGO_CON_PORCENTAJE]
AS
BEGIN
    SELECT id, formapago, 
           ISNULL(porcentaje_recargo, 0) as porcentajeRecargo,
           ISNULL(senia_minima, 0) as seniaMinima 
    FROM formaspago 
    ORDER BY id;
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_TRAERIDCLIENTEXCODIGO]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_TRAERIDCLIENTEXCODIGO]
(
	@PID INT OUTPUT,
	@PCODIGO INT
)
AS
BEGIN
	
	SELECT @PID = id FROM clientes where codigo = @PCODIGO;

END;
GO
/****** Object:  StoredProcedure [dbo].[SP_TRAERLOCALIDADES]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_TRAERLOCALIDADES]
AS
BEGIN

	SELECT id, localidad FROM localidades;

END;
GO
/****** Object:  StoredProcedure [dbo].[SP_TRAERNEXTCODIGOCLIENTE]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_TRAERNEXTCODIGOCLIENTE]
(
	@PCODIGO INT OUTPUT
)
AS
BEGIN
	
	SELECT @PCODIGO = ISNULL(MAX(codigo), 0) + 1 FROM clientes;

END;
GO
/****** Object:  StoredProcedure [dbo].[SP_TRAERPROVEEDORES]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_TRAERPROVEEDORES]
AS
BEGIN
    SELECT 
        id,
        proveedor as nombre,
        contacto,
        telefono,
        email,
        direccion,
        observaciones,
        activo
    FROM proveedores
    ORDER BY activo DESC, proveedor ASC;
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_TRAERULTIMOCODIGOLIBRO]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_TRAERULTIMOCODIGOLIBRO]
(
	@PCODIGO INT OUTPUT
)
AS
BEGIN
	
	SELECT @PCODIGO = ISNULL(MAX(codigo), 0) + 1 FROM libros;

END;
GO
/****** Object:  StoredProcedure [dbo].[SP_VERIFICARPAGORESERVAS]    Script Date: 22/7/2025 15:59:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_VERIFICARPAGORESERVAS]
(
    @PRESERVAS_IDS NVARCHAR(1000)
)
AS
BEGIN
    SELECT TOP 1 
        id,
        monto,
        metodo_pago,
        estado_pago,
        fecha_pago,
        preference_id,
        payment_id,
        external_reference,
        reservas_ids
    FROM pagos 
    WHERE reservas_ids = @PRESERVAS_IDS 
      AND estado_pago = 'approved' 
      AND activo = 1
    ORDER BY fecha_pago DESC;
END;
GO
USE [master]
GO
ALTER DATABASE [LibreriaFarolito] SET  READ_WRITE 
GO
