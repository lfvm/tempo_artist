//Archivo para manejar las fucniones de usuarios en la api
// Como crear, actualizar, eliminar y obtener usuarios
//Llamar estas funciones en las rutas corresqondientes
const connection = require('../db/db_config');

const getAllUsers = (req,res) => {
    connection.query(`SELECT * FROM USUARIOS`, (err, rows, fields) => {
        
        if (!err) {
            res.json({
                status: 'success', 
                rows
            });

        } else {

            res.json({
                status: 'fail', 
                'message': err
            });
        }
    });
}


const getUserById = (req,res) => {

    const id = req.params.id;
    connection.query(`SELECT * FROM usuarios WHERE (id_usaurio=${id})`, (err, rows, fields) => {
        
        if (!err) {

            if (rows.length < 1){

                return res.json({
                    status: 'fail', 
                    msg: "no user with the given id"
                });

            }

            const user = rows[0]
            return res.json({
                status: 'success', 
                user
            });

        } else {

            return res.json({
                status: 'fail', 
                'message': err
            });
        }
    });
}


const createUser =  async( req, res ) => {

    //Guardar el usuaior en la base de datos

    let cliente = 'cliente'
    //Obtener fecha y tiempo actual
    let today = new Date().toISOString().slice(0, 19).replace('T', ' ');

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
    )VALUES('${user.nombre}','${user.apellidos}','${user.correo}','${user.password}','${user.gender}','${today}','${cliente}','${user.edad}',${true});`


    connection.query(query, function(err, rows, fields) {

        if (!err) {

                  
            return res.json({
                status: "succes",
                user, 
                id: rows.insertId
            });

        }
            
        return res.json({
            status: "fail",
            err
        });
    });
        

  
        
    
   

}

const logUserIn = async(req,res) => {


    const {correo ,password} = req.body;

    const query = `SELECT * FROM usuarios WHERE 
    (correo='${correo}' AND contraseña='${password}');
    `


    connection.query(query, function(err, rows, fields) {

        if (!err) {
             //Verfifcar que exista un usuario con ese mail y contraseña
            if(rows.length < 1 ){

                return res.json({
                    status: "fail",
                    msg: "Incorrect email or password"
                });

            }

            //Mandar mensaje de exito
            const user = rows[0]
            return res.json({

                status: "succes",
                user
            });
        
        }   
        
        console.log( err);
        return res.json({
            status: "fail",
            error
        });

    });
        




}


const updateUser = async(req,res) => {

    const id = req.params.id;
    return res.json({
        status: "succes",
        id
    });
}

module.exports = {

    createUser,
    logUserIn,
    getAllUsers,
    getUserById,
    updateUser 

}