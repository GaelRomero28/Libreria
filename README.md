# Base de Datos: Librería

## Crear la base de datos

```sql
CREATE DATABASE db_libreria;
```

## Usar la base de datos

```sql
USE db_libreria;
```

---

# Crear tablas

## Tabla: Autores

```sql
CREATE TABLE tb_autores (
    id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    autor NVARCHAR(150) NOT NULL,
    estatus INT NOT NULL DEFAULT 1,
    fecha_registro DATETIME DEFAULT GETDATE() NOT NULL
);
```

## Tabla: Géneros

```sql
CREATE TABLE tb_generos (
    id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
    genero NVARCHAR(150) NOT NULL,
    estatus INT NOT NULL DEFAULT 1,
    fecha_registro DATETIME DEFAULT GETDATE() NOT NULL
);
```

## Tabla: Libros

```sql
CREATE TABLE tb_libros (
    id_libro VARCHAR(5) PRIMARY KEY NOT NULL,
    titulo NVARCHAR(150) NOT NULL,
    anio_publicacion INT NOT NULL,
    id_autor INT FOREIGN KEY REFERENCES tb_autores(id),
    id_genero INT FOREIGN KEY REFERENCES tb_generos(id),
    estatus INT NOT NULL DEFAULT 1,
    fecha_registro DATETIME DEFAULT GETDATE() NOT NULL
);
```

---

# Insertar datos

## Insertar géneros

```sql
INSERT INTO tb_generos (genero)
VALUES
('Fantasía'),
('Terror'),
('Ciencia Ficción'),
('Romance'),
('Fantasía Épica'),
('Drama');
```

## Insertar autores

```sql
INSERT INTO tb_autores (autor)
VALUES
('J.R.R. Tolkien'),
('Gabriel García Márquez'),
('Stephen King'),
('J.K. Rowling');
```

---

# Consultas

## Ver géneros

```sql
SELECT * FROM tb_generos;
```

## Ver autores

```sql
SELECT * FROM tb_autores;
```
