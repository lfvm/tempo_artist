//Archivo para manejar las fucniones de usuarios en la api
// Como crear, actualizar, eliminar y obtener usuarios
//Llamar estas funciones en las rutas corresqondientes
const connection = require('../db/db_config');

const createUser =  async( req, res ) => {

    //Guardar el usuaior en la base de datos

    let date;
    let cliente = 'cliente'
    let today = new Date().toLocaleDateString()

    let user  = req.body;


    const query = `INSERT INTO usuarios (
        nombre,
        apellidos,
        correo,
        contraseña,
        genero,
        fecha_creacion,
        rol,
        edad,
        toca_instrumento
    )VALUES('${user.nombre}','${user.apellidos}','${user.correo}','${user.password}','${user.gender}','${date}','${cliente}','${user.edad}',${true});`

    try {

        await connection.query(query, function(err, rows, fields) {

            if (err) {
    
                console.log( err);
                connection.destroy();
                return res.json({
                    msg: "error",
                    error
                });
            }
            
            return res.json({
                msg: "ok",
                rows
            });
        });
        
    } catch (error) {

        return res.json({
            msg: "error",
            error
        });
        
    }
   

}

const logUserIn = async(req,res) => {
//


}

module.exports = {

    createUser

}