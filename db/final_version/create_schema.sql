CREATE TABLE IF NOT EXISTS Persons (
    user_id int NOT NULL AUTO_INCREMENT UNIQUE,
    name varchar(255) NOT NULL,
    last_name varchar(255),
    age int NOT NULL,
    gender varchar(10) NOT NULL,
    plays_instrument bool not null,
    created_at datetime not null,
    password varchar(255) not null,
    mail varchar(255) not null unique,
    role varchar(10) not null,
    PRIMARY KEY (user_id)
);

CREATE TABLE IF NOT EXISTS Levels (
    level_id int NOT NULL AUTO_INCREMENT UNIQUE,
    lenght int not null,
	name varchar(255) unique not null,
    difficulty int not null,
    total_notes int not null,
	PRIMARY KEY (level_id)
);


CREATE TABLE IF NOT EXISTS Scores (
    score_id int NOT NULL AUTO_INCREMENT UNIQUE,
    user_id  int not null,
    level_id int not null,
    total_points int not null,
    created_at datetime not null,
    perfect_hits int not null,
	good_hits int not null,
	accuracy int not null,
    max_combo int not null,
	PRIMARY KEY (score_id),
	FOREIGN KEY (user_id) REFERENCES Persons(user_id),
	FOREIGN KEY (level_id) REFERENCES Levels(level_id)
);


ALTER TABLE persons
RENAME TO Users;
