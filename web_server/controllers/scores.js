const connection = require("../db/db_config")


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



const getUserScores = (req, res) => {

    const id = req.params.id;

    const query = `
    SELECT total_points, perfect_hits, good_hits, Levels.total_notes, Scores.created_at, accuracy, max_combo, Levels.name  
    FROM Users INNER JOIN  Scores 
        USING(user_id)
        INNER JOIN levels
        USING(level_id)
        where user_id = ${id}
        ORDER BY total_points DESC;
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