const connection = require("../db/db_config")

//Funcion para obtener todas las puntuaciones
const getAllPunctuations = (req, res) => {

    connection.query(`SELECT * FROM Scores`, (err, rows, fields) => {
        
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



//FUncion para crear puntuacion
const createPunctuation = (req, res) => {

    const score = req.body;
    let today = new Date().toISOString().slice(0, 19).replace('T', ' ');


    const query = `
    INSERT INTO Scores 
    (
        user_id,
        level_id,
        total_points,
        created_at,
        perfect_hits,
        good_hits,
        accuracy,
        max_combo
    ) VALUES (
        '${score.user_id}',
        '${score.level_id}',
        '${score.total_points}',
        '${today}',
        '${score.perfect_hits}',
        '${score.good_hits}',
        '${score.accuracy}',
        '${score.max_combo}'
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


//Funcion para obtener las puntuaciones de un usuario por su id 
const getUserScores = (req, res) => {

    const id = req.params.id;

    //Query que consulta la vista user_stats de la db
    const query = `
    SELECT * FROM user_stats WHERE user_id = ${id};
    `
    connection.query(query, (err, rows, fields) => {
        
        if (!err) {


            res.json({
                status: 'success', 
                scores: rows
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
    getUserScores
}