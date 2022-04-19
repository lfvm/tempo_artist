const connection = require("../db/db_config")


const getAllPunctuations = (req, res) => {

    connection.query(`SELECT * FROM puntuaciones`, (err, rows, fields) => {
        
        if (!err) {

            punctuations = rows;

            res.json({
                status: 'success', 
                punctuations
            });

        } else {

            res.json({
                status: 'fail', 
                err
            });
        }
    });

}




const createPunctuation = (req, res) => {

    const punctuation = req.body;
    let today = new Date().toISOString().slice(0, 19).replace('T', ' ');


    const query = `
    INSERT INTO puntuaciones 
    (
        id_usuario,
        id_nivel,
        total_puntos,
        fecha
    ) VALUES (
        '${punctuation.id_usuario}',
        '${punctuation.id_nivel}',
        '${punctuation.total_puntos}',
        '${today}'
    );
    `

    connection.query(query, (err, rows, fields) => {
        
        if (!err) {


            res.json({
                status: 'success', 
                rows,
                id: rows.insertId
            });

        } else {

            res.json({
                status: 'fail', 
                err
            });
        }
    });


}






module.exports = {
    createPunctuation,
    getAllPunctuations,
}