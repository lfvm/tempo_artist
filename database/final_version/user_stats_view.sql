 CREATE VIEW user_stats AS
 SELECT user_id, total_points, perfect_hits, good_hits, Levels.total_notes, Scores.created_at, accuracy, max_combo, Levels.name  
    FROM Users INNER JOIN  Scores 
        USING(user_id)
        INNER JOIN levels
        USING(level_id)
        ORDER BY total_points DESC;