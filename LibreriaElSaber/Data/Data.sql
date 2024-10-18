--TRUNCATE TABLE Libros
INSERT INTO Libros (Titulo, Autor, ISBN, Genero, Editorial, AnioPublicacion, CantidadDisponible, CantidadTotal, Imagen) VALUES
('Cien años de soledad', 'Gabriel García Márquez', '978-3-16-148410-0', 'Novela', 'Editorial XYZ', 1967, 5, 10, 'http://localhost:5249/images/book1.png'),
('El amor en los tiempos del cólera', 'Gabriel García Márquez', '978-3-16-148410-1', 'Novela', 'Editorial XYZ', 1985, 3, 5, 'http://localhost:5249/images/book2.png'),
('La sombra del viento', 'Carlos Ruiz Zafón', '978-3-16-148410-2', 'Novela', 'Editorial ABC', 2001, 4, 8, 'http://localhost:5249/images/book3.png'),
('1984', 'George Orwell', '978-3-16-148410-3', 'Distopía', 'Editorial DEF', 1949, 10, 15, 'http://localhost:5249/images/book4.png'),
('El principito', 'Antoine de Saint-Exupéry', '978-3-16-148410-4', 'Infantil', 'Editorial GHI', 1943, 7, 12, 'http://localhost:5249/images/book5.png'),
('Crónicas de una muerte anunciada', 'Gabriel García Márquez', '978-3-16-148410-5', 'Novela', 'Editorial XYZ', 1981, 2, 4, 'http://localhost:5249/images/book6.png'),
('Fahrenheit 451', 'Ray Bradbury', '978-3-16-148410-6', 'Ciencia Ficción', 'Editorial XYZ', 1953, 8, 11, 'http://localhost:5249/images/book7.png'),
('El gran Gatsby', 'F. Scott Fitzgerald', '978-3-16-148410-7', 'Novela', 'Editorial DEF', 1925, 9, 15, 'http://localhost:5249/images/book8.png'),
('Orgullo y prejuicio', 'Jane Austen', '978-3-16-148410-8', 'Romance', 'Editorial ABC', 1813, 5, 10, 'http://localhost:5249/images/book9.png'),
('Don Quijote de la Mancha', 'Miguel de Cervantes', '978-3-16-148410-9', 'Clásico', 'Editorial GHI', 1605, 6, 14, 'http://localhost:5249/images/book10.png');

--TRUNCATE TABLE Usuarios
INSERT INTO Usuarios (Nombre, CorreoElectronico, Contrasena, TipoUsuario, Imagen) VALUES
('Juan Pérez', 'juan.perez@example.com', 'pass1234', 'usuario', 'http://localhost:5249/images/user1.png'),
('Ana Gómez', 'ana.gomez@example.com', 'pass1234', 'usuario', 'http://localhost:5249/images/user2.png'),
('Carlos Ruiz', 'carlos.ruiz@example.com', 'pass1234', 'administrador', 'http://localhost:5249/images/user3.png'),
('María López', 'maria.lopez@example.com', 'pass1234', 'usuario', 'http://localhost:5249/images/user4.png'),
('Pedro Martínez', 'pedro.martinez@example.com', 'pass1234', 'usuario', 'http://localhost:5249/images/user5.png'),
('Lucía Fernández', 'lucia.fernandez@example.com', 'pass1234', 'usuario', 'http://localhost:5249/images/user6.png'),
('Javier Sánchez', 'javier.sanchez@example.com', 'pass1234', 'administrador', 'http://localhost:5249/images/user7.png'),
('Maria Ruiz', 'maria.ruiz@example.com', 'pass1234', 'usuario', 'http://localhost:5249/images/user8.png'),
('Sofía Morales', 'sofia.morales@example.com', 'pass1234', 'usuario', 'http://localhost:5249/images/user9.png'),
('Andrés Torres', 'andres.torres@example.com', 'pass1234', 'usuario', 'http://localhost:5249/images/user10.png');

--TRUNCATE TABLE Compras
INSERT INTO Compras (IdLibro, IdUsuario, FechaCompra, Precio) VALUES
(1, 1, '2023-01-10', 19.99),
(2, 1, '2023-01-15', 29.99),
(1, 2, '2023-01-20', 19.99),
(3, 3, '2023-01-12', 25.50),
(4, 4, '2023-01-18', 15.75),
(5, 5, '2023-01-22', 10.99),
(6, 6, '2023-01-25', 17.50),
(7, 7, '2023-01-28', 22.00),
(2, 8, '2023-02-01', 29.99),
(3, 9, '2023-02-05', 22.50);

--TRUNCATE TABLE Eventos
INSERT INTO Eventos (NombreEvento, Descripcion, FechaEvento, Lugar) VALUES
('Feria del Libro', 'Una feria dedicada a la promoción de libros de diferentes géneros.', '2023-03-10', 'Centro Cultural'),
('Conferencia de Literatura', 'Una conferencia sobre la literatura contemporánea.', '2023-05-15', 'Auditorio Principal'),
('Taller de Escritura', 'Taller para aprender técnicas de escritura creativa.', '2023-06-20', 'Biblioteca Municipal'),
('Cine y Literatura', 'Proyección de películas basadas en obras literarias.', '2023-07-25', 'Sala de Cine'),
('Noche de Cuentos', 'Lectura de cuentos para los más pequeños.', '2023-08-30', 'Parque Central'),
('Presentación de Libro', 'Lanzamiento del nuevo libro de autor local.', '2023-09-10', 'Librería Local'),
('Encuentro de Poetas', 'Un encuentro para celebrar la poesía.', '2023-10-05', 'Casa de la Cultura'),
('Festival Literario', 'Un festival que une a escritores y lectores.', '2023-11-12', 'Plaza Principal'),
('Feria de las Editoras', 'Un espacio para conocer editoriales independientes.', '2023-12-15', 'Centro de Exposiciones'),
('Charla sobre Nuevas Narrativas', 'Discusión sobre nuevas formas de contar historias.', '2023-01-05', 'Sala de Conferencias');


--TRUNCATE TABLE InscripcionesEventos
INSERT INTO InscripcionesEventos (IdUsuario, IdEvento) VALUES
(1, 1),
(1, 2),
(2, 1),
(3, 3),
(3, 4),
(4, 5),
(5, 6),
(6, 7),
(7, 8),
(8, 1);

--TRUNCATE TABLE Prestamos
INSERT INTO Prestamos (IdLibro, IdUsuario, FechaPrestamo, FechaDevolucion, Devuelto) VALUES
(1, 1, '2023-01-10', '2023-01-17', false),
(2, 1, '2023-01-12', '2023-01-19', true),
(1, 2, '2023-01-15', '2023-01-22', false),
(3, 3, '2023-01-18', '2023-01-25', false),
(4, 4, '2023-01-20', '2023-01-27', true),
(5, 5, '2023-01-22', '2023-01-29', false),
(6, 6, '2023-01-25', '2023-02-01', false),
(7, 7, '2023-02-01', '2023-02-08', true),
(2, 8, '2023-02-05', '2023-02-12', false),
(3, 9, '2023-02-10', '2023-02-17', false);
