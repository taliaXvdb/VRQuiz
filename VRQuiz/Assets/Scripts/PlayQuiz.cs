using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayQuiz : MonoBehaviour
{
    public string _category;
    public int _answerTime;
    public string _difficulty;
    public bool _narrator;
    private bool _isInstructionsShown = true;
    private List<Question> _questions;
    private SetupQuiz _setupQuiz;
    private int _currentQuestionIndex = 1;
    private bool _isQuizStarted = false;
    public int Score = 0;
    public int Answer = 0;
    [SerializeField] private Canvas _instructionsCanvas;
    [SerializeField] private Canvas _quizCanvas;
    [SerializeField] private Canvas _scoreCanvas;
    [SerializeField] private Canvas _nextCanvas;
    [SerializeField] private AudioSource _audioSourceWrong;
    [SerializeField] private AudioSource _audioSourceCorrect;
    [SerializeField] private AudioSource _audioSourceSadTrumpet;
    [SerializeField] private AudioSource _audioSourceApplause;
    [SerializeField] private AudioSource _audioSourceCountdown;

    TMP_Text questionTitle;
    TMP_Text questionText;
    TMP_Text answer1;
    TMP_Text answer2;
    TMP_Text answer3;
    TMP_Text answer4;
    TMP_Text scoreTitle;
    TMP_Text score;

    void Start()
    {
        _setupQuiz = FindObjectOfType<SetupQuiz>();
        _setupQuiz.enabled = false;
        _currentQuestionIndex = 1; // Reset to the first question

        questionTitle = _quizCanvas.transform.Find("QuestionTitle").GetComponent<TMP_Text>();
        questionText = _quizCanvas.transform.Find("QuestionText").GetComponent<TMP_Text>();
        answer1 = _quizCanvas.transform.Find("Answer1").GetComponentInChildren<TMP_Text>();
        answer2 = _quizCanvas.transform.Find("Answer2").GetComponentInChildren<TMP_Text>();
        answer3 = _quizCanvas.transform.Find("Answer3").GetComponentInChildren<TMP_Text>();
        answer4 = _quizCanvas.transform.Find("Answer4").GetComponentInChildren<TMP_Text>();

        scoreTitle = _scoreCanvas.transform.Find("ScoreTitle").GetComponent<TMP_Text>();
        score = _scoreCanvas.transform.Find("Score").GetComponent<TMP_Text>();

        AudioManager audioManager = GameObject.FindObjectOfType<AudioManager>();
        if (audioManager != null)
        {
            audioManager.PlayPlaySound();
        }

        _audioSourceCountdown.Play();
        ShowInstructions();
        PrepareQuestions();

    }

    void Update()
    {
        if (!_isInstructionsShown && _isQuizStarted)
        {
            _quizCanvas.gameObject.SetActive(true);
            _isQuizStarted = false;

            StartQuiz();
        }
    }

    public void PrepareQuestions()
    {
        Debug.Log($"Playing quiz with settings: Answer Time: {_answerTime}, Difficulty: {_difficulty}, Category: {_category}, Narrator: {_narrator}");
        if (_difficulty == "Easy")
        {
            MakeQuestionList("Easy", _category);
            Debug.Log("Easy mode selected.");
        }
        else if (_difficulty == "Normal")
        {
            MakeQuestionList("Normal", _category);
            Debug.Log("Normal mode selected.");
        }
        else if (_difficulty == "Hard")
        {
            MakeQuestionList("Hard", _category);
            Debug.Log("Hard mode selected.");
        }
        else
        {
            Debug.LogWarning("Invalid difficulty selected.");
        }
    }

    void MakeQuestionList(string diff, string category)
    {
        Debug.Log("Making question list...");
        if (diff == "Easy")
        {
            if (category == "Books")
            {
                List<Question> questions = new List<Question>
                {
                    // Easy Questions
                    new Question(
                        "Who is the main character in the Harry Potter series, who goes to a magical school?",
                        new List<string> { "Ron Weasley", "Draco Malfoy", "Hermione Granger", "Harry Potter" },
                        "Harry Potter"
                    ),
                    new Question(
                        "What book series features Katniss Everdeen fighting in a deadly competition?",
                        new List<string> { "Twilight", "The Hunger Games", "The Maze Runner", "Divergent" },
                        "The Hunger Games"
                    ),
                    new Question(
                        "Who wrote the Diary of a Wimpy Kid series?",
                        new List<string> { "Dav Pilkey", "Rick Riordan", "Roald Dahl", "Jeff Kinney" },
                        "Jeff Kinney"
                    ),
                    new Question(
                        "In Percy Jackson and the Olympians, what Greek god is Percy’s father?",
                        new List<string> { "Zeus", "Poseidon", "Apollo", "Hades" },
                        "Poseidon"
                    ),
                    new Question(
                        "What color is the dress on the cover of the book The Selection?",
                        new List<string> { "Red", "Purple", "Green", "Blue" },
                        "Blue"
                    ),
                    new Question(
                        "Who wrote The Fault in Our Stars, a book about two teens dealing with cancer?",
                        new List<string> { "Rainbow Rowell", "Nicholas Sparks", "John Green", "Sarah Dessen" },
                        "John Green"
                    ),
                    new Question(
                        "What’s the title of the book where a spider writes words in her web to save a pig?",
                        new List<string> { "Stuart Little", "Charlotte's Web", "Babe", "Animal Farm" },
                        "Charlotte's Web"
                    ),
                    new Question(
                        "What book series has the tagline, “One ring to rule them all”?",
                        new List<string> { "Percy Jackson", "The Lord of the Rings", "Harry Potter", "The Chronicles of Narnia" },
                        "The Lord of the Rings"
                    ),
                    new Question(
                        "In Twilight, what is the name of Bella’s vampire boyfriend?",
                        new List<string> { "Edward Cullen", "Jacob Black", "Jasper Hale", "Emmett Cullen" },
                        "Edward Cullen"
                    ),
                    new Question(
                        "What is the first book in the Divergent series?",
                        new List<string> { "Allegiant", "Insurgent", "The Hunger Games", "Divergent" },
                        "Divergent"
                    ),
                };
                _questions = questions;
            }
            else if (category == "Food")
            {
                List<Question> questions = new List<Question>
                {
                    new Question(
                        "What fruit is known as the king of fruits?",
                        new List<string> { "Banana", "Apple", "Mango", "Pineapple" },
                        "Mango"
                    ),
                    new Question(
                        "What is the main ingredient in guacamole?",
                        new List<string> { "Lime", "Avocado", "Tomato", "Onion" },
                        "Avocado"
                    ),
                    new Question(
                        "Which drink is known as the 'champagne of teas'?",
                        new List<string> { "Darjeeling Tea", "Green Tea", "Chamomile Tea", "Oolong Tea" },
                        "Darjeeling Tea"
                    ),
                    new Question(
                        "What type of pasta is shaped like little rice grains?",
                        new List<string> { "Fusilli", "Orzo", "Penne", "Linguine" },
                        "Orzo"
                    ),
                    new Question(
                        "What is the name of the food wrap made from dried seaweed?",
                        new List<string> { "Tortilla", "Nori", "Pita", "Rice Paper" },
                        "Nori"
                    ),
                    new Question(
                        "Which fruit is traditionally used in Banoffee Pie?",
                        new List<string> { "Cherry", "Pear", "Banana", "Apple" },
                        "Banana"
                    ),
                    new Question(
                        "What dairy product is made by churning cream?",
                        new List<string> { "Cheese", "Butter", "Sour Cream", "Yogurt" },
                        "Butter"
                    ),
                    new Question(
                        "Which type of nut is used to make Nutella?",
                        new List<string> { "Cashew", "Almond", "Peanut", "Hazelnut" },
                        "Hazelnut"
                    ),
                    new Question(
                        "What vegetable is known as 'courgette' in the UK?",
                        new List<string> { "Eggplant", "Cucumber", "Zucchini", "Bell Pepper" },
                        "Zucchini"
                    ),
                    new Question(
                        "What is the main ingredient in hummus?",
                        new List<string> { "Chickpeas", "Beans", "Lentils", "Peas" },
                        "Chickpeas"
                    ),
                };
                _questions = questions;
            }
            else if (category == "Animals")
            {
                List<Question> questions = new List<Question>
                {
                    new Question(
                        "What is the fastest land animal?",
                        new List<string> { "Lion", "Cheetah", "Tiger", "Gazelle" },
                        "Cheetah"
                    ),
                    new Question(
                        "Which animal is known as the 'King of the Jungle'?",
                        new List<string> { "Tiger", "Elephant", "Lion", "Panther" },
                        "Lion"
                    ),
                    new Question(
                        "What is the tallest animal in the world?",
                        new List<string> { "Elephant", "Giraffe", "Ostrich", "Horse" },
                        "Giraffe"
                    ),
                    new Question(
                        "What is a baby sheep called?",
                        new List<string> { "Kid", "Foal", "Lamb", "Calf" },
                        "Lamb"
                    ),
                    new Question(
                        "What do pandas primarily eat?",
                        new List<string> { "Fish", "Bamboo", "Grass", "Fruits" },
                        "Bamboo"
                    ),
                    new Question(
                        "Which bird is often associated with delivering babies in folklore?",
                        new List<string> { "Swan", "Stork", "Pelican", "Duck" },
                        "Stork"
                    ),
                    new Question(
                        "Which animal is known for changing its color to blend with its surroundings?",
                        new List<string> { "Chameleon", "Gecko", "Iguana", "Frog" },
                        "Chameleon"
                    ),
                    new Question(
                        "What is the largest type of big cat?",
                        new List<string> { "Lion", "Tiger", "Leopard", "Jaguar" },
                        "Tiger"
                    ),
                    new Question(
                        "Which ocean animal has eight legs?",
                        new List<string> { "Jellyfish", "Octopus", "Starfish", "Crab" },
                        "Octopus"
                    ),
                    new Question(
                        "What type of animal is a Komodo dragon?",
                        new List<string> { "Snake", "Lizard", "Crocodile", "Turtle" },
                        "Lizard"
                    ),
                };
                _questions = questions;
            }
            else if (category == "Art")
            {
                List<Question> questions = new List<Question>
                {
                    new Question(
                        "Who painted the Mona Lisa?",
                        new List<string> { "Claude Monet", "Vincent van Gogh", "Pablo Picasso", "Leonardo da Vinci" },
                        "Leonardo da Vinci"
                    ),
                    new Question(
                        "What type of paint is known for being water-soluble and quick-drying?",
                        new List<string> { "Watercolor", "Gouache", "Oil Paint", "Acrylic Paint" },
                        "Acrylic Paint"
                    ),
                    new Question(
                        "Which artist is famous for painting the ceiling of the Sistine Chapel?",
                        new List<string> { "Donatello", "Raphael", "Leonardo da Vinci", "Michelangelo" },
                        "Michelangelo"
                    ),
                    new Question(
                        "What color do you get when you mix blue and yellow?",
                        new List<string> { "Purple", "Green", "Orange", "Brown" },
                        "Green"
                    ),
                    new Question(
                        "What is the term for a painting done on wet plaster?",
                        new List<string> { "Relief", "Mosaic", "Tapestry", "Fresco" },
                        "Fresco"
                    ),
                    new Question(
                        "What is the most common material used for sculpting in ancient Greece?",
                        new List<string> { "Bronze", "Marble", "Wood", "Clay" },
                        "Marble"
                    ),
                    new Question(
                        "What is the name of the museum that houses the Mona Lisa?",
                        new List<string> { "The Prado", "The Louvre", "The British Museum", "The Met" },
                        "The Louvre"
                    ),
                    new Question(
                        "Which artist is famous for his 'Starry Night' painting?",
                        new List<string> { "Claude Monet", "Vincent van Gogh", "Salvador Dalí", "Edvard Munch" },
                        "Vincent van Gogh"
                    ),
                    new Question(
                        "What is the term for art made using small pieces of colored glass or stone?",
                        new List<string> { "Carving", "Mosaic", "Collage", "Fresco" },
                        "Mosaic"
                    ),
                    new Question(
                        "Which art movement is associated with paintings of light and everyday scenes?",
                        new List<string> { "Cubism", "Impressionism", "Baroque", "Romanticism" },
                        "Impressionism"
                    ),
                };
                _questions = questions;
            }
            else if (category == "Geography")
            {
                List<Question> questions = new List<Question>
                {
                    new Question(
                        "What is the largest country in the world by land area?",
                        new List<string> { "China", "Canada", "United States", "Russia" },
                        "Russia"
                    ),
                    new Question(
                        "What is the capital city of France?",
                        new List<string> { "Madrid", "Rome", "Berlin", "Paris" },
                        "Paris"
                    ),
                    new Question(
                        "Which continent is known as the 'Land Down Under'?",
                        new List<string> { "South America", "Asia", "Australia", "Africa" },
                        "Australia"
                    ),
                    new Question(
                        "What is the longest river in the world?",
                        new List<string> { "Yangtze River", "Mississippi River", "Amazon River", "Nile River" },
                        "Amazon River"
                    ),
                    new Question(
                        "What is the name of the desert that covers much of northern Africa?",
                        new List<string> { "Atacama Desert", "Gobi Desert", "Sahara Desert", "Kalahari Desert" },
                        "Sahara Desert"
                    ),
                    new Question(
                        "What is the smallest country in the world?",
                        new List<string> { "Monaco", "San Marino", "Vatican City", "Liechtenstein" },
                        "Vatican City"
                    ),
                    new Question(
                        "Which ocean is the largest?",
                        new List<string> { "Atlantic Ocean", "Indian Ocean", "Arctic Ocean", "Pacific Ocean" },
                        "Pacific Ocean"
                    ),
                    new Question(
                        "What is the capital city of Japan?",
                        new List<string> { "Seoul", "Beijing", "Bangkok", "Tokyo" },
                        "Tokyo"
                    ),
                    new Question(
                        "Which U.S. state is known as the Sunshine State?",
                        new List<string> { "California", "Arizona", "Texas", "Florida" },
                        "Florida"
                    ),
                    new Question(
                        "What mountain range separates Europe and Asia?",
                        new List<string> { "Himalayas", "Andes", "Ural Mountains", "Rocky Mountains" },
                        "Ural Mountains"
                    ),
                };
                _questions = questions;
            }
            else if (category == "History")
            {
                List<Question> questions = new List<Question>
                {
                    new Question(
                        "Who was the first president of the United States?",
                        new List<string> { "Andrew Jackson", "Abraham Lincoln", "Thomas Jefferson", "George Washington" },
                        "George Washington"
                    ),
                    new Question(
                        "In which year did the Titanic sink?",
                        new List<string> { "1905", "1920", "1898", "1912" },
                        "1912"
                    ),
                    new Question(
                        "Who was the famous queen of Egypt?",
                        new List<string> { "Sakhmet", "Nefertiti", "Hatshepsut", "Cleopatra" },
                        "Cleopatra"
                    ),
                    new Question(
                        "Which country did the United States gain independence from in 1776?",
                        new List<string> { "France", "Germany", "Spain", "Great Britain" },
                        "Great Britain"
                    ),
                    new Question(
                        "Which war was fought between the North and South in the United States?",
                        new List<string> { "World War I", "The Civil War", "The Revolutionary War", "The War of 1812" },
                        "The Civil War"
                    ),
                    new Question(
                        "Who was the first woman to fly solo across the Atlantic Ocean?",
                        new List<string> { "Harriet Tubman", "Amelia Earhart", "Bessie Coleman", "Eleanor Roosevelt" },
                        "Amelia Earhart"
                    ),
                    new Question(
                        "What was the name of the ship that brought the Pilgrims to America in 1620?",
                        new List<string> { "Beagle", "Santa Maria", "Nina", "Mayflower" },
                        "Mayflower"
                    ),
                    new Question(
                        "Which U.S. president issued the Emancipation Proclamation?",
                        new List<string> { "Franklin D. Roosevelt", "Theodore Roosevelt", "Abraham Lincoln", "Andrew Johnson" },
                        "Abraham Lincoln"
                    ),
                    new Question(
                        "What year did World War II end?",
                        new List<string> { "1941", "1945", "1939", "1940" },
                        "1945"
                    ),
                    new Question(
                        "Which country was the first to land a man on the moon?",
                        new List<string> { "China", "India", "Russia", "USA" },
                        "USA"
                    )
                };
                _questions = questions;
            }
            else if (category == "Movies")
            {
                List<Question> questions = new List<Question>
                {
                    new Question(
                        "Who directed the movie 'Jurassic Park'?",
                        new List<string> { "George Lucas", "Quentin Tarantino", "Steven Spielberg", "Martin Scorsese" },
                        "Steven Spielberg"
                    ),
                    new Question(
                        "What is the name of the wizard in 'Harry Potter'?",
                        new List<string> { "Harry Potter", "Frodo Baggins", "Luke Skywalker", "Tony Stark" },
                        "Harry Potter"
                    ),
                    new Question(
                        "Which animated movie features a fish named Marlin searching for his son?",
                        new List<string> { "Shrek", "Toy Story", "The Lion King", "Finding Nemo" },
                        "Finding Nemo"
                    ),
                    new Question(
                        "In which movie does a young boy talk to a robot named WALL-E?",
                        new List<string> { "Frozen", "WALL-E", "Up", "Monsters, Inc." },
                        "WALL-E"
                    ),
                    new Question(
                        "Which superhero has the alter ego 'Bruce Wayne'?",
                        new List<string> { "Iron Man", "Batman", "Spider-Man", "Superman" },
                        "Batman"
                    ),
                    new Question(
                        "What year did the movie 'Titanic' release?",
                        new List<string> { "2001", "1999", "2005", "1997" },
                        "1997"
                    ),
                    new Question(
                        "Which animated movie features a snowman named Olaf?",
                        new List<string> { "Moana", "Shrek", "Frozen", "Zootopia" },
                        "Frozen"
                    ),
                    new Question(
                        "Who played Jack Dawson in 'Titanic'?",
                        new List<string> { "Tom Cruise", "Brad Pitt", "Johnny Depp", "Leonardo DiCaprio" },
                        "Leonardo DiCaprio"
                    ),
                    new Question(
                        "What movie franchise is about a 'ring' with great power?",
                        new List<string> { "Star Wars", "Pirates of the Caribbean", "Harry Potter", "The Lord of the Rings" },
                        "The Lord of the Rings"
                    ),
                    new Question(
                        "Which movie features a group of superheroes called the Avengers?",
                        new List<string> { "X-Men", "Guardians of the Galaxy", "The Avengers", "Justice League" },
                        "The Avengers"
                    )
                };
                _questions = questions;
            }
            else if (category == "Music")
            {
                List<Question> questions = new List<Question>
                {
                    new Question(
                        "Who is known as the 'King of Pop'?",
                        new List<string> { "Freddie Mercury", "John Lennon", "Michael Jackson", "Elvis Presley" },
                        "Michael Jackson"
                    ),
                    new Question(
                        "Which band is famous for the song 'Hey Jude'?",
                        new List<string> { "Queen", "Led Zeppelin", "The Beatles", "The Rolling Stones" },
                        "The Beatles"
                    ),
                    new Question(
                        "What instrument does Taylor Swift play?",
                        new List<string> { "Piano", "Violin", "Guitar", "Drums" },
                        "Guitar"
                    ),
                    new Question(
                        "Which song is sung by Ed Sheeran?",
                        new List<string> { "Little Things", "Shape of You", "Uptown Funk", "Despacito" },
                        "Shape of You"
                    ),
                    new Question(
                        "Which of these is a song by Adele?",
                        new List<string> { "Rolling in the Deep", "All of the above", "Hello", "Someone Like You" },
                        "All of the above"
                    ),
                    new Question(
                        "Who sang 'Like a Rolling Stone'?",
                        new List<string> { "Jimi Hendrix", "Elvis Presley", "Bob Dylan", "John Lennon" },
                        "Bob Dylan"
                    ),
                    new Question(
                        "What genre is most associated with artists like Drake and Lil Wayne?",
                        new List<string> { "Pop", "Jazz", "Country", "Rap" },
                        "Rap"
                    ),
                    new Question(
                        "Which pop star is known for the hit 'Bad Romance'?",
                        new List<string> { "Beyoncé", "Ariana Grande", "Lady Gaga", "Katy Perry" },
                        "Lady Gaga"
                    ),
                    new Question(
                        "Which instrument does Paul McCartney famously play in The Beatles?",
                        new List<string> { "Bass guitar", "Guitar", "Drums", "Piano" },
                        "Bass guitar"
                    ),
                    new Question(
                        "Who is known for the hit song 'Uptown Funk'?",
                        new List<string> { "Justin Timberlake", "Pharrell Williams", "Jay-Z", "Bruno Mars" },
                        "Bruno Mars"
                    )
                };
                _questions = questions;
            }
            else if (category == "Science")
            {
                List<Question> questions = new List<Question>
                {
                    new Question(
                        "What is the chemical symbol for water?",
                        new List<string> { "H2O", "O2", "CO2", "H2O2" },
                        "H2O"
                    ),
                    new Question(
                        "What planet is known as the 'Red Planet'?",
                        new List<string> { "Mars", "Venus", "Earth", "Jupiter" },
                        "Mars"
                    ),
                    new Question(
                        "How many legs do insects have?",
                        new List<string> { "8", "6", "4", "10" },
                        "6"
                    ),
                    new Question(
                        "What is the hardest natural substance on Earth?",
                        new List<string> { "Gold", "Diamond", "Iron", "Silver" },
                        "Diamond"
                    ),
                    new Question(
                        "What is the boiling point of water in Celsius?",
                        new List<string> { "100°C", "0°C", "50°C", "150°C" },
                        "100°C"
                    ),
                    new Question(
                        "What gas do plants absorb from the air?",
                        new List<string> { "Oxygen", "Nitrogen", "Carbon Dioxide", "Hydrogen" },
                        "Carbon Dioxide"
                    ),
                    new Question(
                        "What is the largest organ in the human body?",
                        new List<string> { "Heart", "Liver", "Skin", "Lungs" },
                        "Skin"
                    ),
                    new Question(
                        "Which element is the most abundant in the Earth's crust?",
                        new List<string> { "Oxygen", "Iron", "Carbon", "Aluminum" },
                        "Oxygen"
                    ),
                    new Question(
                        "What is the main source of energy for the Earth?",
                        new List<string> { "The Moon", "The Sun", "The Stars", "The Earth itself" },
                        "The Sun"
                    ),
                    new Question(
                        "Which animal is known for having a long neck?",
                        new List<string> { "Elephant", "Giraffe", "Horse", "Bear" },
                        "Giraffe"
                    )
                };
                _questions = questions;
            }
            else if (category == "Sport")
            {
                List<Question> questions = new List<Question>
                {
                    new Question(
                        "Which sport is known as 'the king of sports'?",
                        new List<string> { "Basketball", "Tennis", "Soccer", "Baseball" },
                        "Soccer"
                    ),
                    new Question(
                        "How many players are there on a standard basketball team?",
                        new List<string> { "9", "7", "5", "11" },
                        "5"
                    ),
                    new Question(
                        "Which country is famous for its sport, cricket?",
                        new List<string> { "USA", "Canada", "Germany", "India" },
                        "India"
                    ),
                    new Question(
                        "What color is the puck used in ice hockey?",
                        new List<string> { "Blue", "White", "Red", "Black" },
                        "Black"
                    ),
                    new Question(
                        "Which sport is played at Wimbledon?",
                        new List<string> { "Golf", "Football", "Baseball", "Tennis" },
                        "Tennis"
                    ),
                    new Question(
                        "What is the maximum number of players on a soccer field at once?",
                        new List<string> { "22", "18", "16", "20" },
                        "22"
                    ),
                    new Question(
                        "In which sport do you perform a slam dunk?",
                        new List<string> { "Soccer", "Tennis", "Football", "Basketball" },
                        "Basketball"
                    ),
                    new Question(
                        "Which sport uses a bat and ball and is played on a diamond-shaped field?",
                        new List<string> { "Rugby", "Golf", "Cricket", "Baseball" },
                        "Baseball"
                    ),
                    new Question(
                        "Which Olympic sport involves throwing a discus?",
                        new List<string> { "Cycling", "Track and Field", "Swimming", "Archery" },
                        "Track and Field"
                    ),
                    new Question(
                        "Which sport involves the Tour de France?",
                        new List<string> { "Rugby", "Running", "Cycling", "Swimming" },
                        "Cycling"
                    )
                };
                _questions = questions;
            }
        }
        else if (diff == "Normal")
        {
            if (category == "Books")
            {
                List<Question> questions = new List<Question>
                {
                    new Question(
                        "Who is the author of Looking for Alaska?",
                        new List<string> { "Suzanne Collins", "Stephen King", "John Green", "J.K. Rowling" },
                        "John Green"
                    ),
                    new Question(
                        "In the Harry Potter series, what house is Draco Malfoy in?",
                        new List<string> { "Gryffindor", "Ravenclaw", "Slytherin", "Hufflepuff" },
                        "Slytherin"
                    ),
                    new Question(
                        "What is the dystopian city called in The Maze Runner series?",
                        new List<string> { "The Glade", "Panem", "District 12", "The Capitol" },
                        "The Glade"
                    ),
                    new Question(
                        "Who wrote the novel Eleanor & Park, about two misfit teens falling in love?",
                        new List<string> { "Cassandra Clare", "Rainbow Rowell", "John Green", "Nicholas Sparks" },
                        "Rainbow Rowell"
                    ),
                    new Question(
                        "In the Hunger Games, what is the name of Katniss’s home district?",
                        new List<string> { "District 13", "District 5", "District 12", "District 1" },
                        "District 12"
                    ),
                    new Question(
                        "What book follows Starr Carter as she deals with police violence and activism?",
                        new List<string> { "All American Boys", "Dear Martin", "The Hate U Give", "Angie Thomas" },
                        "The Hate U Give"
                    ),
                    new Question(
                        "In Divergent, what faction values intelligence above all?",
                        new List<string> { "Amity", "Candor", "Erudite", "Dauntless" },
                        "Erudite"
                    ),
                    new Question(
                        "Who wrote the novel Wonder, about a boy with a facial difference?",
                        new List<string> { "Sarah Weeks", "R.J. Palacio", "John Green", "Jodi Picoult" },
                        "R.J. Palacio"
                    ),
                    new Question(
                        "What is the title of the first book in A Series of Unfortunate Events?",
                        new List<string> { "The Austere Academy", "The Reptile Room", "The Wide Window", "The Bad Beginning" },
                        "The Bad Beginning"
                    ),
                    new Question(
                        "What’s the name of the magical wardrobe world in C.S. Lewis’s book?",
                        new List<string> { "Neverland", "Narnia", "Hogwarts", "Middle-earth" },
                        "Narnia"
                    ),
                };
                _questions = questions;
            }
            else if (category == "Food")
            {
                List<Question> questions = new List<Question>
                {
                    new Question(
                        "What is the Japanese dish consisting of thinly sliced raw fish?",
                        new List<string> { "Ramen", "Sushi", "Sashimi", "Tempura" },
                        "Sashimi"
                    ),
                    new Question(
                        "Which spice is known as 'golden spice' due to its color and health benefits?",
                        new List<string> { "Cinnamon", "Turmeric", "Saffron", "Cumin" },
                        "Turmeric"
                    ),
                    new Question(
                        "What type of cheese is traditionally used on a margherita pizza?",
                        new List<string> { "Ricotta", "Cheddar", "Mozzarella", "Parmesan" },
                        "Mozzarella"
                    ),
                    new Question(
                        "Which country is famous for its Gouda cheese?",
                        new List<string> { "Italy", "France", "Switzerland", "Netherlands" },
                        "Netherlands"
                    ),
                    new Question(
                        "What is the name of the small, salty fish often used as pizza topping?",
                        new List<string> { "Anchovy", "Herring", "Sardine", "Mackerel" },
                        "Anchovy"
                    ),
                    new Question(
                        "What is the name of the French soup made from puréed leeks, potatoes, and cream?",
                        new List<string> { "Bouillabaisse", "Vichyssoise", "Bisque", "Consommé" },
                        "Vichyssoise"
                    ),
                    new Question(
                        "What is the primary ingredient in pesto sauce?",
                        new List<string> { "Basil", "Spinach", "Cilantro", "Parsley" },
                        "Basil"
                    ),
                    new Question(
                        "Which Italian dish is made of layers of pasta, meat sauce, and béchamel?",
                        new List<string> { "Gnocchi", "Risotto", "Lasagna", "Cannelloni" },
                        "Lasagna"
                    ),
                    new Question(
                        "Which dessert is traditionally set on fire before serving?",
                        new List<string> { "Crème Brûlée", "Baked Alaska", "Tiramisu", "Pavlova" },
                        "Baked Alaska"
                    ),
                    new Question(
                        "What grain is used to make couscous?",
                        new List<string> { "Millet", "Quinoa", "Semolina", "Barley" },
                        "Semolina"
                    ),
                };
                _questions = questions;
            }
            else if (category == "Animals")
            {
                List<Question> questions = new List<Question>
                {
                    new Question(
                        "What is the only mammal capable of true flight?",
                        new List<string> { "Bat", "Flying Squirrel", "Pigeon", "Eagle" },
                        "Bat"
                    ),
                    new Question(
                        "What is the name of a group of lions?",
                        new List<string> { "Flock", "Herd", "Pride", "Pack" },
                        "Pride"
                    ),
                    new Question(
                        "What is the largest species of shark?",
                        new List<string> { "Hammerhead Shark", "Tiger Shark", "Whale Shark", "Great White Shark" },
                        "Whale Shark"
                    ),
                    new Question(
                        "Which animal has the longest lifespan?",
                        new List<string> { "Elephant", "Giant Tortoise", "Blue Whale", "Macaw" },
                        "Giant Tortoise"
                    ),
                    new Question(
                        "What is the fastest bird in the world?",
                        new List<string> { "Golden Eagle", "Falcon", "Peregrine Falcon", "Albatross" },
                        "Peregrine Falcon"
                    ),
                    new Question(
                        "What is a group of crows called?",
                        new List<string> { "Flock", "Murder", "Pack", "Colony" },
                        "Murder"
                    ),
                    new Question(
                        "What type of animal is a barracuda?",
                        new List<string> { "Fish", "Amphibian", "Reptile", "Bird" },
                        "Fish"
                    ),
                    new Question(
                        "Which animal is known for having three hearts?",
                        new List<string> { "Octopus", "Jellyfish", "Squid", "Cuttlefish" },
                        "Octopus"
                    ),
                    new Question(
                        "What type of bear is native to China?",
                        new List<string> { "Panda", "Grizzly", "Polar Bear", "Sun Bear" },
                        "Panda"
                    ),
                    new Question(
                        "What is the term for animals that eat both plants and meat?",
                        new List<string> { "Herbivores", "Carnivores", "Omnivores", "Insectivores" },
                        "Omnivores"
                    ),
                };
                _questions = questions;
            }
            else if (category == "Art")
            {
                List<Question> questions = new List<Question>
                {
                    new Question(
                        "What is the name of the technique where paint is applied in small dots to create an image?",
                        new List<string> { "Pointillism", "Expressionism", "Surrealism", "Cubism" },
                        "Pointillism"
                    ),
                    new Question(
                        "Which artist is known for painting melting clocks in 'The Persistence of Memory'?",
                        new List<string> { "René Magritte", "Pablo Picasso", "Joan Miró", "Salvador Dalí" },
                        "Salvador Dalí"
                    ),
                    new Question(
                        "Who painted 'The Girl with a Pearl Earring'?",
                        new List<string> { "Frans Hals", "Johannes Vermeer", "Jan van Eyck", "Rembrandt" },
                        "Johannes Vermeer"
                    ),
                    new Question(
                        "What is the term for a painting or sculpture of a person’s face?",
                        new List<string> { "Still Life", "Abstract", "Portrait", "Landscape" },
                        "Portrait"
                    ),
                    new Question(
                        "Which famous artist is associated with the Cubist movement?",
                        new List<string> { "Henri Matisse", "Vincent van Gogh", "Pablo Picasso", "Gustav Klimt" },
                        "Pablo Picasso"
                    ),
                    new Question(
                        "What is the term for the arrangement of objects in a painting, often of fruit and flowers?",
                        new List<string> { "Abstract", "Still Life", "Landscape", "Fresco" },
                        "Still Life"
                    ),
                    new Question(
                        "Which artist is famous for his colorful water lilies paintings?",
                        new List<string> { "Paul Cézanne", "Claude Monet", "Edgar Degas", "Pierre-Auguste Renoir" },
                        "Claude Monet"
                    ),
                    new Question(
                        "What is the art style characterized by swirling, dreamlike images, as seen in 'The Starry Night'?",
                        new List<string> { "Impressionism", "Post-Impressionism", "Realism", "Expressionism" },
                        "Post-Impressionism"
                    ),
                    new Question(
                        "Which Renaissance artist sculpted David and painted the Sistine Chapel ceiling?",
                        new List<string> { "Donatello", "Raphael", "Michelangelo", "Leonardo da Vinci" },
                        "Michelangelo"
                    ),
                    new Question(
                        "What is the technique of using light and shadow in art to create a sense of depth?",
                        new List<string> { "Pointillism", "Fresco", "Chiaroscuro", "Abstract" },
                        "Chiaroscuro"
                    ),
                };
                _questions = questions;
            }
            else if (category == "Geography")
            {
                List<Question> questions = new List<Question>
                {
                    new Question(
                        "What is the name of the imaginary line that divides the Earth into the Northern and Southern Hemispheres?",
                        new List<string> { "Prime Meridian", "International Date Line", "Tropic of Cancer", "Equator" },
                        "Equator"
                    ),
                    new Question(
                        "Which African country is completely surrounded by South Africa?",
                        new List<string> { "Botswana", "Eswatini", "Namibia", "Lesotho" },
                        "Lesotho"
                    ),
                    new Question(
                        "Which country has the most natural lakes in the world?",
                        new List<string> { "Russia", "Finland", "United States", "Canada" },
                        "Canada"
                    ),
                    new Question(
                        "What is the capital city of Brazil?",
                        new List<string> { "São Paulo", "Rio de Janeiro", "Salvador", "Brasília" },
                        "Brasília"
                    ),
                    new Question(
                        "What is the largest island in the world?",
                        new List<string> { "Borneo", "Australia", "New Guinea", "Greenland" },
                        "Greenland"
                    ),
                    new Question(
                        "Which country is known as the Land of the Rising Sun?",
                        new List<string> { "South Korea", "China", "Japan", "Thailand" },
                        "Japan"
                    ),
                    new Question(
                        "What is the capital city of Canada?",
                        new List<string> { "Montreal", "Toronto", "Ottawa", "Vancouver" },
                        "Ottawa"
                    ),
                    new Question(
                        "What is the name of the sea located between Europe and Africa?",
                        new List<string> { "Mediterranean Sea", "Black Sea", "Red Sea", "Caribbean Sea" },
                        "Mediterranean Sea"
                    ),
                    new Question(
                        "What is the name of the tallest mountain in Africa?",
                        new List<string> { "Mount Kenya", "Mount Stanley", "Mount Kilimanjaro", "Rwenzori Mountains" },
                        "Mount Kilimanjaro"
                    ),
                    new Question(
                        "Which European country is shaped like a boot?",
                        new List<string> { "Greece", "Spain", "Portugal", "Italy" },
                        "Italy"
                    ),
                };
                _questions = questions;
            }
            else if (category == "History")
            {
                List<Question> questions = new List<Question>
                {
                    new Question(
                        "Who was the leader of Nazi Germany during World War II?",
                        new List<string> { "Joseph Stalin", "Winston Churchill", "Adolf Hitler", "Benito Mussolini" },
                        "Adolf Hitler"
                    ),
                    new Question(
                        "In which year did World War I begin?",
                        new List<string> { "1918", "1939", "1905", "1914" },
                        "1914"
                    ),
                    new Question(
                        "What ancient civilization built the pyramids in Egypt?",
                        new List<string> { "Romans", "Babylonians", "Ancient Egyptians", "Greeks" },
                        "Ancient Egyptians"
                    ),
                    new Question(
                        "Who discovered the Americas in 1492?",
                        new List<string> { "Christopher Columbus", "Ferdinand Magellan", "John Cabot", "Marco Polo" },
                        "Christopher Columbus"
                    ),
                    new Question(
                        "What was the name of the ship that brought the Pilgrims to America in 1620?",
                        new List<string> { "Santa Maria", "Beagle", "Mayflower", "Nina" },
                        "Mayflower"
                    ),
                    new Question(
                        "Who was the famous queen of ancient Egypt?",
                        new List<string> { "Sakhmet", "Cleopatra", "Nefertiti", "Hatshepsut" },
                        "Cleopatra"
                    ),
                    new Question(
                        "Who was the first emperor of China?",
                        new List<string> { "Qin Shi Huang", "Li Shimin", "Han Wudi", "Emperor Wu of Han" },
                        "Qin Shi Huang"
                    ),
                    new Question(
                        "Which empire was ruled by Julius Caesar?",
                        new List<string> { "Roman Empire", "Ottoman Empire", "Byzantine Empire", "Mongol Empire" },
                        "Roman Empire"
                    ),
                    new Question(
                        "In which year did the French Revolution begin?",
                        new List<string> { "1776", "1812", "1799", "1789" },
                        "1789"
                    ),
                    new Question(
                        "Which war was fought between the North and South in the United States?",
                        new List<string> { "The War of 1812", "World War I", "The Revolutionary War", "The Civil War" },
                        "The Civil War"
                    )
                };
                _questions = questions;
            }
            else if (category == "Movies")
            {
                List<Question> questions = new List<Question>
                {
                    new Question(
                        "Who directed the movie 'Inception'?",
                        new List<string> { "Quentin Tarantino", "Christopher Nolan", "Martin Scorsese", "Steven Spielberg" },
                        "Christopher Nolan"
                    ),
                    new Question(
                        "Which actor played the role of 'Iron Man' in the Marvel movies?",
                        new List<string> { "Robert Downey Jr.", "Chris Hemsworth", "Mark Ruffalo", "Chris Evans" },
                        "Robert Downey Jr."
                    ),
                    new Question(
                        "What movie features a character called 'Forrest Gump'?",
                        new List<string> { "The Green Mile", "The Pursuit of Happyness", "Forrest Gump", "Shawshank Redemption" },
                        "Forrest Gump"
                    ),
                    new Question(
                        "Which movie tells the story of a young woman named Katniss Everdeen?",
                        new List<string> { "The Hunger Games", "Twilight", "Maze Runner", "Divergent" },
                        "The Hunger Games"
                    ),
                    new Question(
                        "Which director is known for the movie 'Pulp Fiction'?",
                        new List<string> { "James Cameron", "George Lucas", "Steven Spielberg", "Quentin Tarantino" },
                        "Quentin Tarantino"
                    ),
                    new Question(
                        "Which movie features the iconic quote 'Here's looking at you, kid'?",
                        new List<string> { "Gone with the Wind", "Citizen Kane", "The Godfather", "Casablanca" },
                        "Casablanca"
                    ),
                    new Question(
                        "What is the name of the fictional African country in 'Black Panther'?",
                        new List<string> { "Zamunda", "Elbonia", "Genovia", "Wakanda" },
                        "Wakanda"
                    ),
                    new Question(
                        "In which movie did Tom Hanks star as a man stranded on an island?",
                        new List<string> { "Forrest Gump", "Saving Private Ryan", "Cast Away", "The Terminal" },
                        "Cast Away"
                    ),
                    new Question(
                        "Which movie features a robot named R2-D2?",
                        new List<string> { "I, Robot", "The Terminator", "Star Wars", "Blade Runner" },
                        "Star Wars"
                    ),
                    new Question(
                        "Which animated movie features a clownfish named Marlin searching for his son?",
                        new List<string> { "Finding Nemo", "Zootopia", "Shrek", "Toy Story" },
                        "Finding Nemo"
                    )
                };
                _questions = questions;
            }
            else if (category == "Music")
            {
                List<Question> questions = new List<Question>
                {
                    new Question(
                        "Which of these bands is known for the song 'Smells Like Teen Spirit'?",
                        new List<string> { "Pearl Jam", "Soundgarden", "Nirvana", "Green Day" },
                        "Nirvana"
                    ),
                    new Question(
                        "Which music festival is held annually in Indio, California?",
                        new List<string> { "Lollapalooza", "Coachella", "Tomorrowland", "Glastonbury" },
                        "Coachella"
                    ),
                    new Question(
                        "Which singer performed the song 'I Will Always Love You'?",
                        new List<string> { "Alicia Keys", "Whitney Houston", "Mariah Carey", "Celine Dion" },
                        "Whitney Houston"
                    ),
                    new Question(
                        "Which classical composer created the famous 'Symphony No. 5'?",
                        new List<string> { "Johann Sebastian Bach", "Frédéric Chopin", "Ludwig van Beethoven", "Wolfgang Amadeus Mozart" },
                        "Ludwig van Beethoven"
                    ),
                    new Question(
                        "Who is known for the album 'The Dark Side of the Moon'?",
                        new List<string> { "Pink Floyd", "Led Zeppelin", "Queen", "The Beatles" },
                        "Pink Floyd"
                    ),
                    new Question(
                        "Which musical artist is known for the album 'Born to Run'?",
                        new List<string> { "Tom Petty", "Billy Joel", "Bruce Springsteen", "Bob Dylan" },
                        "Bruce Springsteen"
                    ),
                    new Question(
                        "Who is the lead singer of U2?",
                        new List<string> { "Bruce Springsteen", "Mick Jagger", "Freddie Mercury", "Bono" },
                        "Bono"
                    ),
                    new Question(
                        "What is the name of Beyoncé's alter ego?",
                        new List<string> { "Destiny", "Lemonade", "Sasha Fierce", "Queen B" },
                        "Sasha Fierce"
                    ),
                    new Question(
                        "Which song by Michael Jackson became known as the first music video with a high budget?",
                        new List<string> { "Billie Jean", "Thriller", "Beat It", "Bad" },
                        "Thriller"
                    ),
                    new Question(
                        "Who released the hit album '1989'?",
                        new List<string> { "Ariana Grande", "Katy Perry", "Lady Gaga", "Taylor Swift" },
                        "Taylor Swift"
                    )
                };
                _questions = questions;
            }
            else if (category == "Science")
            {
                List<Question> questions = new List<Question>
                {
                    new Question(
                        "What is the powerhouse of the cell?",
                        new List<string> { "Nucleus", "Mitochondria", "Ribosome", "Endoplasmic Reticulum" },
                        "Mitochondria"
                    ),
                    new Question(
                        "Who developed the theory of relativity?",
                        new List<string> { "Isaac Newton", "Albert Einstein", "Galileo Galilei", "Niels Bohr" },
                        "Albert Einstein"
                    ),
                    new Question(
                        "What is the chemical formula for methane?",
                        new List<string> { "CH4", "CO2", "H2O", "C6H12O6" },
                        "CH4"
                    ),
                    new Question(
                        "What is the process by which plants make their own food?",
                        new List<string> { "Respiration", "Fermentation", "Photosynthesis", "Digestion" },
                        "Photosynthesis"
                    ),
                    new Question(
                        "What planet is closest to the Sun?",
                        new List<string> { "Earth", "Venus", "Mercury", "Mars" },
                        "Mercury"
                    ),
                    new Question(
                        "What is the chemical symbol for gold?",
                        new List<string> { "Au", "Ag", "Pb", "Fe" },
                        "Au"
                    ),
                    new Question(
                        "What is the main gas found in the air we breathe?",
                        new List<string> { "Oxygen", "Carbon Dioxide", "Nitrogen", "Hydrogen" },
                        "Nitrogen"
                    ),
                    new Question(
                        "Which element is the most abundant in the human body?",
                        new List<string> { "Oxygen", "Carbon", "Hydrogen", "Nitrogen" },
                        "Oxygen"
                    ),
                    new Question(
                        "Which part of the plant conducts photosynthesis?",
                        new List<string> { "Roots", "Stem", "Leaves", "Flowers" },
                        "Leaves"
                    ),
                    new Question(
                        "What is the main function of red blood cells?",
                        new List<string> { "Fight infections", "Transport oxygen", "Digest food", "Store energy" },
                        "Transport oxygen"
                    )
                };
                _questions = questions;
            }
            else if (category == "Sport")
            {
                List<Question> questions = new List<Question>
                {
                    new Question(
                        "Who holds the record for the most goals scored in World Cup history?",
                        new List<string> { "Miroslav Klose", "Cristiano Ronaldo", "Pele", "Marta" },
                        "Miroslav Klose"
                    ),
                    new Question(
                        "Which country won the 2018 FIFA World Cup?",
                        new List<string> { "Brazil", "Germany", "France", "Argentina" },
                        "France"
                    ),
                    new Question(
                        "In tennis, what is the term for a score of 40-40?",
                        new List<string> { "Love", "Break", "Deuce", "Advantage" },
                        "Deuce"
                    ),
                    new Question(
                        "In which sport would you perform a 'knockout'?",
                        new List<string> { "Football", "Boxing", "Tennis", "Wrestling" },
                        "Boxing"
                    ),
                    new Question(
                        "Who is known as 'The Greatest' in boxing?",
                        new List<string> { "Floyd Mayweather", "George Foreman", "Mike Tyson", "Muhammad Ali" },
                        "Muhammad Ali"
                    ),
                    new Question(
                        "What is the standard length of a basketball court?",
                        new List<string> { "28 meters", "24 meters", "30 meters", "22 meters" },
                        "28 meters"
                    ),
                    new Question(
                        "Which sport is played with a shuttlecock?",
                        new List<string> { "Baseball", "Tennis", "Badminton", "Soccer" },
                        "Badminton"
                    ),
                    new Question(
                        "Which country won the 2008 Summer Olympics?",
                        new List<string> { "China", "USA", "Germany", "Russia" },
                        "China"
                    ),
                    new Question(
                        "What is the name of the annual tennis tournament held in Australia?",
                        new List<string> { "US Open", "French Open", "Australian Open", "Wimbledon" },
                        "Australian Open"
                    ),
                    new Question(
                        "Which country is famous for the sport of rugby?",
                        new List<string> { "Spain", "New Zealand", "France", "USA" },
                        "New Zealand"
                    )
                };
                _questions = questions;
            }
        }
        else if (diff == "Hard")
        {
            if (category == "Books")
            {
                List<Question> questions = new List<Question>
                {
                    new Question(
                        "Who is the author of They Both Die at the End, a book about two teens who meet on their last day alive?",
                        new List<string> { "John Green", "Nicola Yoon", "Adam Silvera", "Becky Albertalli" },
                        "Adam Silvera"
                    ),
                    new Question(
                        "In Shadow and Bone, what is the name of the dark magical force Alina Starkov faces?",
                        new List<string> { "The Rift", "The Darkness", "The Fold", "The Shadow Realm" },
                        "The Fold"
                    ),
                    new Question(
                        "What is the real identity of the anonymous blogger in Gossip Girl (book series)?",
                        new List<string> { "Blair Waldorf", "Chuck Bass", "Dan Humphrey", "Serena van der Woodsen" },
                        "Dan Humphrey"
                    ),
                    new Question(
                        "In Six of Crows, what is Kaz Brekker’s nickname?",
                        new List<string> { "The Thief King", "The Bastard of the Barrel", "Dirtyhands", "The Shadow" },
                        "Dirtyhands"
                    ),
                    new Question(
                        "Who wrote One of Us Is Lying, a mystery about four teens under suspicion of murder?",
                        new List<string> { "R.L. Stine", "Holly Jackson", "Karen M. McManus", "Sarah Dessen" },
                        "Karen M. McManus"
                    ),
                    new Question(
                        "What is the name of the boarding school in Crescent City: House of Earth and Blood by Sarah J. Maas?",
                        new List<string> { "Mistwood", "Bryce Quinlan doesn’t attend a school", "The House of Fae", "Velaris Academy" },
                        "Bryce Quinlan doesn’t attend a school"
                    ),
                    new Question(
                        "In The Cruel Prince, who is Jude’s primary rival in the faerie court?",
                        new List<string> { "Madoc", "Taryn", "Cardan", "Nicasia" },
                        "Cardan"
                    ),
                    new Question(
                        "What is the name of the group of teen vigilantes in Renegades by Marissa Meyer?",
                        new List<string> { "The Watchers", "The Renegades", "The Enforcers", "The Anarchists" },
                        "The Renegades"
                    ),
                    new Question(
                        "In Red Queen, what power does Mare Barrow discover she has?",
                        new List<string> { "Shapeshifting", "Healing powers", "Controlling electricity", "Reading minds" },
                        "Controlling electricity"
                    ),
                    new Question(
                        "Who wrote We Were Liars, a book about a privileged but broken family on a private island?",
                        new List<string> { "Sarah Dessen", "E. Lockhart", "John Green", "Jennifer Niven" },
                        "E. Lockhart"
                    ),
                };
                _questions = questions;
            }
            else if (category == "Food")
            {
                List<Question> questions = new List<Question>
                {
                    new Question(
                        "What is the name of the French delicacy made of duck or goose liver?",
                        new List<string> { "Confit", "Terrine", "Pâté", "Foie Gras" },
                        "Foie Gras"
                    ),
                    new Question(
                        "Which type of rice is traditionally used to make risotto?",
                        new List<string> { "Jasmine", "Basmati", "Arborio", "Carnaroli" },
                        "Arborio"
                    ),
                    new Question(
                        "What is the term for the Japanese art of arranging food on a plate?",
                        new List<string> { "Bento", "Ikebana", "Washoku", "Morigami" },
                        "Morigami"
                    ),
                    new Question(
                        "Which country is the origin of the dish 'kimchi'?",
                        new List<string> { "China", "South Korea", "Japan", "Thailand" },
                        "South Korea"
                    ),
                    new Question(
                        "What is the main ingredient in the Scandinavian dish 'lutefisk'?",
                        new List<string> { "Rice", "Beef", "Fish", "Potatoes" },
                        "Fish"
                    ),
                    new Question(
                        "What is the name of the dish made of snails in French cuisine?",
                        new List<string> { "Coq au Vin", "Ratatouille", "Escargot", "Bouillabaisse" },
                        "Escargot"
                    ),
                    new Question(
                        "Which fruit is known for containing an enzyme that tenderizes meat?",
                        new List<string> { "Mango", "Papaya", "Kiwi", "Pineapple" },
                        "Pineapple"
                    ),
                    new Question(
                        "What is the chemical responsible for the spiciness in chili peppers?",
                        new List<string> { "Capsaicin", "Allicin", "Curcumin", "Piperine" },
                        "Capsaicin"
                    ),
                    new Question(
                        "What is the name of the Italian bread that is typically seasoned with olive oil and herbs?",
                        new List<string> { "Ciabatta", "Panettone", "Brioche", "Focaccia" },
                        "Focaccia"
                    ),
                    new Question(
                        "What is the name of the fermented soybean paste used in Korean cooking?",
                        new List<string> { "Miso", "Tahini", "Tempeh", "Doenjang" },
                        "Doenjang"
                    ),
                };
                _questions = questions;
            }
            else if (category == "Animals")
            {
                List<Question> questions = new List<Question>
                {
                    new Question(
                        "What is the largest land carnivore?",
                        new List<string> { "Lion", "Grizzly Bear", "Polar Bear", "Tiger" },
                        "Polar Bear"
                    ),
                    new Question(
                        "What is the name of the world’s smallest bird?",
                        new List<string> { "Bee Hummingbird", "Sparrow", "Warbler", "Finch" },
                        "Bee Hummingbird"
                    ),
                    new Question(
                        "What type of animal is an axolotl?",
                        new List<string> { "Amphibian", "Fish", "Reptile", "Mammal" },
                        "Amphibian"
                    ),
                    new Question(
                        "What is the scientific term for warm-blooded animals?",
                        new List<string> { "Endothermic", "Exothermic", "Thermogenic", "Ectothermic" },
                        "Endothermic"
                    ),
                    new Question(
                        "Which marine animal is known to have the most powerful bite?",
                        new List<string> { "Shark", "Crocodile", "Killer Whale", "Hippo" },
                        "Crocodile"
                    ),
                    new Question(
                        "What is the name of the organ that allows birds to produce sound?",
                        new List<string> { "Larynx", "Syrinx", "Bronchus", "Trachea" },
                        "Syrinx"
                    ),
                    new Question(
                        "Which animal is known for having the most number of teeth?",
                        new List<string> { "Shark", "Snail", "Alligator", "Catfish" },
                        "Snail"
                    ),
                    new Question(
                        "What is the only venomous primate?",
                        new List<string> { "Slow Loris", "Tarsier", "Capuchin Monkey", "Baboon" },
                        "Slow Loris"
                    ),
                    new Question(
                        "Which animal can survive without water for its entire life?",
                        new List<string> { "Desert Kangaroo Rat", "Camel", "Jerboa", "Sand Cat" },
                        "Desert Kangaroo Rat"
                    ),
                    new Question(
                        "What type of animal is a dugong?",
                        new List<string> { "Mammal", "Fish", "Reptile", "Amphibian" },
                        "Mammal"
                    ),
                };
                _questions = questions;
            }
            else if (category == "Art")
            {
                List<Question> questions = new List<Question>
                {
                    new Question(
                        "Which artist is known for the painting 'Las Meninas'?",
                        new List<string> { "El Greco", "Diego Velázquez", "Francisco Goya", "Salvador Dalí" },
                        "Diego Velázquez"
                    ),
                    new Question(
                        "What is the name of the surrealist artist famous for his 'Time Transfixed' painting?",
                        new List<string> { "Salvador Dalí", "Max Ernst", "René Magritte", "Man Ray" },
                        "René Magritte"
                    ),
                    new Question(
                        "Which modern art movement focused on geometric shapes and abstract forms?",
                        new List<string> { "Surrealism", "Cubism", "Dadaism", "Futurism" },
                        "Cubism"
                    ),
                    new Question(
                        "What is the art of arranging colors and textures in textiles called?",
                        new List<string> { "Mosaic", "Weaving", "Embroidery", "Tapestry" },
                        "Tapestry"
                    ),
                    new Question(
                        "Who painted 'The Garden of Earthly Delights'?",
                        new List<string> { "Pieter Bruegel", "Caravaggio", "Hieronymus Bosch", "Jan van Eyck" },
                        "Hieronymus Bosch"
                    ),
                    new Question(
                        "What is the term for a painting made entirely of shades of one color?",
                        new List<string> { "Minimalist", "Monochrome", "Duotone", "Abstract" },
                        "Monochrome"
                    ),
                    new Question(
                        "Which artist is known for his depictions of ballerinas?",
                        new List<string> { "Claude Monet", "Edgar Degas", "Henri Matisse", "Auguste Rodin" },
                        "Edgar Degas"
                    ),
                    new Question(
                        "What is the name of the famous woodblock print depicting a wave?",
                        new List<string> { "The Great Wave off Kanagawa", "Ocean in Motion", "Japanese Tide", "Mount Fuji Wave" },
                        "The Great Wave off Kanagawa"
                    ),
                    new Question(
                        "What is the term for the architectural style characterized by flying buttresses and stained glass windows?",
                        new List<string> { "Romanesque", "Baroque", "Gothic", "Renaissance" },
                        "Gothic"
                    ),
                    new Question(
                        "Who created the controversial sculpture 'Fountain' using a urinal?",
                        new List<string> { "Salvador Dalí", "Marcel Duchamp", "Man Ray", "Pablo Picasso" },
                        "Marcel Duchamp"
                    ),
                };
                _questions = questions;
            }
            else if (category == "Geography")
            {
                List<Question> questions = new List<Question>
                {
                    new Question(
                        "What is the name of the largest desert in Asia?",
                        new List<string> { "Karakum Desert", "Taklamakan Desert", "Gobi Desert", "Thar Desert" },
                        "Gobi Desert"
                    ),
                    new Question(
                        "Which river flows through Baghdad?",
                        new List<string> { "Euphrates River", "Jordan River", "Tigris River", "Nile River" },
                        "Tigris River"
                    ),
                    new Question(
                        "What is the southernmost city in the world?",
                        new List<string> { "Rio Gallegos", "Ushuaia", "Puerto Williams", "Punta Arenas" },
                        "Puerto Williams"
                    ),
                    new Question(
                        "What is the name of the deepest ocean trench in the world?",
                        new List<string> { "Philippine Trench", "Tonga Trench", "Mariana Trench", "Kuril-Kamchatka Trench" },
                        "Mariana Trench"
                    ),
                    new Question(
                        "Which country has the highest number of UNESCO World Heritage Sites?",
                        new List<string> { "China", "Spain", "Italy", "India" },
                        "Italy"
                    ),
                    new Question(
                        "Which mountain range is home to Mount Everest?",
                        new List<string> { "Alps", "Andes", "Himalayas", "Rocky Mountains" },
                        "Himalayas"
                    ),
                    new Question(
                        "What is the smallest continent by land area?",
                        new List<string> { "Antarctica", "South America", "Europe", "Australia" },
                        "Australia"
                    ),
                    new Question(
                        "What is the name of the ocean current that warms Western Europe?",
                        new List<string> { "Agulhas Current", "Canary Current", "Kuroshio Current", "Gulf Stream" },
                        "Gulf Stream"
                    ),
                    new Question(
                        "Which country is home to the Atacama Desert, the driest place on Earth?",
                        new List<string> { "Peru", "Bolivia", "Chile", "Argentina" },
                        "Chile"
                    ),
                    new Question(
                        "What is the capital of the Maldives?",
                        new List<string> { "Colombo", "Port Louis", "Malé", "Dhaka" },
                        "Malé"
                    ),
                };
                _questions = questions;
            }
            else if (category == "History")
            {
                List<Question> questions = new List<Question>
                {
                    new Question(
                        "Who was the first emperor of China?",
                        new List<string> { "Han Wudi", "Li Shimin", "Qin Shi Huang", "Emperor Wu of Han" },
                        "Qin Shi Huang"
                    ),
                    new Question(
                        "What was the name of the treaty that ended World War I?",
                        new List<string> { "Treaty of Paris", "Treaty of Ghent", "Treaty of Versailles", "Treaty of Tordesillas" },
                        "Treaty of Versailles"
                    ),
                    new Question(
                        "Which battle marked the turning point of the American Civil War?",
                        new List<string> { "Battle of Fort Sumter", "Battle of Antietam", "Battle of Gettysburg", "Battle of Bull Run" },
                        "Battle of Gettysburg"
                    ),
                    new Question(
                        "Which country did the Soviet Union invade in 1979, starting the Soviet-Afghan War?",
                        new List<string> { "Pakistan", "Afghanistan", "Iran", "Iraq" },
                        "Afghanistan"
                    ),
                    new Question(
                        "What was the capital of the Byzantine Empire?",
                        new List<string> { "Athens", "Carthage", "Constantinople", "Rome" },
                        "Constantinople"
                    ),
                    new Question(
                        "Who was the British prime minister during World War II?",
                        new List<string> { "Neville Chamberlain", "Tony Blair", "Winston Churchill", "Margaret Thatcher" },
                        "Winston Churchill"
                    ),
                    new Question(
                        "Which was the first country to grant women the right to vote?",
                        new List<string> { "United States", "United Kingdom", "Australia", "New Zealand" },
                        "New Zealand"
                    ),
                    new Question(
                        "Which country was the first to land a man on the moon?",
                        new List<string> { "China", "India", "Russia", "USA" },
                        "USA"
                    ),
                    new Question(
                        "What is the largest empire in history by land area?",
                        new List<string> { "Roman Empire", "Ottoman Empire", "Mongol Empire", "British Empire" },
                        "British Empire"
                    ),
                    new Question(
                        "Who was the leader of the Soviet Union during World War II?",
                        new List<string> { "Leon Trotsky", "Joseph Stalin", "Nikita Khrushchev", "Vladimir Lenin" },
                        "Joseph Stalin"
                    )
                };
                _questions = questions;
            }
            else if (category == "Movies")
            {
                List<Question> questions = new List<Question>
                {
                    new Question(
                        "Which director is known for the movie 'The Shining'?",
                        new List<string> { "Stanley Kubrick", "Ridley Scott", "David Lynch", "John Carpenter" },
                        "Stanley Kubrick"
                    ),
                    new Question(
                        "Which movie features a character named 'Travis Bickle'?",
                        new List<string> { "American Psycho", "Taxi Driver", "Fight Club", "The Godfather" },
                        "Taxi Driver"
                    ),
                    new Question(
                        "Who won the Academy Award for Best Actor in 2009?",
                        new List<string> { "Matthew McConaughey", "Leonardo DiCaprio", "Daniel Day-Lewis", "Jeff Bridges" },
                        "Daniel Day-Lewis"
                    ),
                    new Question(
                        "Which 2003 movie was directed by Peter Jackson and is the final installment of a trilogy?",
                        new List<string> { "The Fellowship of the Ring", "The Return of the King", "The Two Towers", "King Kong" },
                        "The Return of the King"
                    ),
                    new Question(
                        "Which actor starred in the movie 'There Will Be Blood'?",
                        new List<string> { "Tom Hanks", "Johnny Depp", "Brad Pitt", "Daniel Day-Lewis" },
                        "Daniel Day-Lewis"
                    ),
                    new Question(
                        "What is the name of the fictional hotel in 'The Grand Budapest Hotel'?",
                        new List<string> { "The Royal Tenenbaums", "The Plaza Hotel", "The Hotel New Hampshire", "The Grand Budapest Hotel" },
                        "The Grand Budapest Hotel"
                    ),
                    new Question(
                        "Which movie features a fictional country called 'Genovia'?",
                        new List<string> { "The King and I", "Cinderella", "Coming to America", "The Princess Diaries" },
                        "The Princess Diaries"
                    ),
                    new Question(
                        "Which movie, directed by Quentin Tarantino, features the character 'The Bride'?",
                        new List<string> { "Pulp Fiction", "Inglourious Basterds", "Kill Bill", "Jackie Brown" },
                        "Kill Bill"
                    ),
                    new Question(
                        "Which movie was the first to win the Academy Award for Best Picture in color?",
                        new List<string> { "Gone with the Wind", "The Wizard of Oz", "Ben-Hur", "The Sound of Music" },
                        "The Wizard of Oz"
                    ),
                    new Question(
                        "Which 1999 movie features a character named 'The Narrator' played by Edward Norton?",
                        new List<string> { "American History X", "Fight Club", "The Sixth Sense", "The Matrix" },
                        "Fight Club"
                    )
                };
                _questions = questions;
            }
            else if (category == "Music")
            {
                List<Question> questions = new List<Question>
                {
                    new Question(
                        "Who is the composer of the opera 'Carmen'?",
                        new List<string> { "Georges Bizet", "Giuseppe Verdi", "Wolfgang Amadeus Mozart", "Ludwig van Beethoven" },
                        "Georges Bizet"
                    ),
                    new Question(
                        "Which band released the album 'The Wall'?",
                        new List<string> { "Led Zeppelin", "The Rolling Stones", "Pink Floyd", "The Beatles" },
                        "Pink Floyd"
                    ),
                    new Question(
                        "Which artist is known for the album 'To Pimp a Butterfly'?",
                        new List<string> { "Chance the Rapper", "J. Cole", "Kendrick Lamar", "Drake" },
                        "Kendrick Lamar"
                    ),
                    new Question(
                        "Who wrote the song 'Imagine'?",
                        new List<string> { "Paul McCartney", "John Lennon", "George Harrison", "Ringo Starr" },
                        "John Lennon"
                    ),
                    new Question(
                        "Which rock band is known for the song 'Bohemian Rhapsody'?",
                        new List<string> { "The Rolling Stones", "Led Zeppelin", "Queen", "The Who" },
                        "Queen"
                    ),
                    new Question(
                        "What genre of music is associated with The Velvet Underground?",
                        new List<string> { "Blues", "Jazz", "Alternative Rock", "Country" },
                        "Alternative Rock"
                    ),
                    new Question(
                        "Which musician was also known as the 'Queen of Soul'?",
                        new List<string> { "Diana Ross", "Aretha Franklin", "Mavis Staples", "Etta James" },
                        "Aretha Franklin"
                    ),
                    new Question(
                        "Which artist's real name is Robert Zimmerman?",
                        new List<string> { "Bruce Springsteen", "John Lennon", "Bob Dylan", "Tom Petty" },
                        "Bob Dylan"
                    ),
                    new Question(
                        "What was David Bowie's alter ego in the 1970s?",
                        new List<string> { "Aladdin Sane", "Ziggy Stardust", "Major Tom", "The Thin White Duke" },
                        "Ziggy Stardust"
                    ),
                    new Question(
                        "Which jazz legend is known for the album 'Kind of Blue'?",
                        new List<string> { "Miles Davis", "Louis Armstrong", "John Coltrane", "Duke Ellington" },
                        "Miles Davis"
                    )
                };
                _questions = questions;
            }
            else if (category == "Sience")
            {
                List<Question> questions = new List<Question>
                {
                    new Question(
                        "Who is known as the father of modern physics?",
                        new List<string> { "Isaac Newton", "Albert Einstein", "Max Planck", "Nikola Tesla" },
                        "Albert Einstein"
                    ),
                    new Question(
                        "What is the most common isotope of hydrogen?",
                        new List<string> { "Deuterium", "Tritium", "Protium", "Hydrogen-2" },
                        "Protium"
                    ),
                    new Question(
                        "What is the atomic number of carbon?",
                        new List<string> { "12", "6", "8", "14" },
                        "6"
                    ),
                    new Question(
                        "What is the main function of the large intestine?",
                        new List<string> { "Absorb nutrients", "Digest food", "Absorb water", "Produce bile" },
                        "Absorb water"
                    ),
                    new Question(
                        "What is the second most abundant element in the Earth's crust?",
                        new List<string> { "Silicon", "Aluminum", "Iron", "Magnesium" },
                        "Silicon"
                    ),
                    new Question(
                        "Who is credited with discovering the structure of DNA?",
                        new List<string> { "Rosalind Franklin", "James Watson", "Francis Crick", "Maurice Wilkins" },
                        "James Watson"
                    ),
                    new Question(
                        "What type of radiation is used in MRI scans?",
                        new List<string> { "Ultraviolet", "Gamma", "X-rays", "Magnetic fields" },
                        "Magnetic fields"
                    ),
                    new Question(
                        "What is the most common gas in the Earth's atmosphere?",
                        new List<string> { "Carbon Dioxide", "Oxygen", "Nitrogen", "Argon" },
                        "Nitrogen"
                    ),
                    new Question(
                        "What is the element with the highest atomic number in the periodic table?",
                        new List<string> { "Uranium", "Oganesson", "Plutonium", "Neptunium" },
                        "Oganesson"
                    ),
                    new Question(
                        "What is the phenomenon that causes a rainbow?",
                        new List<string> { "Diffraction", "Reflection", "Refraction", "Dispersion" },
                        "Refraction"
                    )
                };
                _questions = questions;
            }
            else if (category == "Sport")
            {
                List<Question> questions = new List<Question>
                {
                    new Question(
                        "Who won the Ballon d'Or in 2020?",
                        new List<string> { "Lionel Messi", "Robert Lewandowski", "Cristiano Ronaldo", "Kevin De Bruyne" },
                        "Robert Lewandowski"
                    ),
                    new Question(
                        "In which year did Usain Bolt set the world record for the 100 meters?",
                        new List<string> { "2009", "2008", "2012", "2016" },
                        "2009"
                    ),
                    new Question(
                        "Which country hosted the 1992 Summer Olympics?",
                        new List<string> { "USA", "Spain", "Australia", "Italy" },
                        "Spain"
                    ),
                    new Question(
                        "Who holds the record for the most Grand Slam singles titles in tennis?",
                        new List<string> { "Roger Federer", "Novak Djokovic", "Rafael Nadal", "Pete Sampras" },
                        "Novak Djokovic"
                    ),
                    new Question(
                        "Which NFL team has won the most Super Bowl titles?",
                        new List<string> { "New England Patriots", "Pittsburgh Steelers", "Dallas Cowboys", "Green Bay Packers" },
                        "New England Patriots"
                    ),
                    new Question(
                        "Who was the first female athlete to earn more than $1 million in a single year?",
                        new List<string> { "Serena Williams", "Venus Williams", "Maria Sharapova", "Billie Jean King" },
                        "Maria Sharapova"
                    ),
                    new Question(
                        "Who won the 2021 UEFA Champions League final?",
                        new List<string> { "Chelsea", "Manchester City", "Bayern Munich", "Paris Saint-Germain" },
                        "Chelsea"
                    ),
                    new Question(
                        "Which golfer has won the most Masters tournaments?",
                        new List<string> { "Tiger Woods", "Phil Mickelson", "Jack Nicklaus", "Arnold Palmer" },
                        "Jack Nicklaus"
                    ),
                    new Question(
                        "Which team won the 2019 Rugby World Cup?",
                        new List<string> { "South Africa", "England", "New Zealand", "Australia" },
                        "South Africa"
                    ),
                    new Question(
                        "Which athlete holds the record for the most Olympic gold medals in a single Games?",
                        new List<string> { "Michael Phelps", "Mark Spitz", "Carl Lewis", "Usain Bolt" },
                        "Michael Phelps"
                    )
                };
                _questions = questions;
            }
        }
    }

    public void StartQuiz()
    {
        Debug.Log("Starting quiz...");
        StartCoroutine(DisplayQuestions());
    }

    public void ShowInstructions()
    {
        _instructionsCanvas.gameObject.SetActive(true);
        StartCoroutine(HideInstructions());
    }

    // Hide the instructions canvas after 10 seconds
    private IEnumerator HideInstructions()
    {
        yield return new WaitForSeconds(3);
        _instructionsCanvas.gameObject.SetActive(false);

        _isQuizStarted = true;
        _isInstructionsShown = false;
    }

    private void ShowQuestion(Question question)
    {
        questionTitle.text = string.Empty;
        questionText.text = string.Empty;
        answer1.text = string.Empty;
        answer2.text = string.Empty;
        answer3.text = string.Empty;
        answer4.text = string.Empty;

        Debug.Log(question.Text);
        questionTitle.text = "Question " + _currentQuestionIndex;
        questionText.text = question.Text;
        answer1.text = question.Answers[0];
        answer2.text = question.Answers[1];
        answer3.text = question.Answers[2];
        answer4.text = question.Answers[3];
        _currentQuestionIndex++;
    }

    private IEnumerator DisplayQuestions()
    {
        foreach (Question question in _questions)
        {
            ShowQuestion(question);
            yield return new WaitForSeconds(_answerTime);
            CheckAnswer(question);
            Answer = 0;
        }

        Debug.Log("Quiz completed!");
        Debug.Log($"Your score: {Score}/{_questions.Count}");

        _quizCanvas.gameObject.SetActive(false);
        _scoreCanvas.gameObject.SetActive(true);
        if (Score == 10)
        {
            scoreTitle.text = "Perfect!";
            score.text = Score + "/" + _questions.Count;
            _audioSourceApplause.Play();
        }
        else if (Score >= 7)
        {
            scoreTitle.text = "Well done!";
            score.text = Score + "/" + _questions.Count;
            _audioSourceApplause.Play();
        }
        else if (Score >= 5)
        {
            scoreTitle.text = "Good effort!";
            score.text = Score + "/" + _questions.Count;
            _audioSourceApplause.Play();
        }
        else
        {
            scoreTitle.text = "Better luck next time!";
            score.text = Score + "/" + _questions.Count;
            _audioSourceSadTrumpet.Play();
        }
    }

    private void CheckAnswer(Question question)
    {
        if (Answer == 0)
        {
            Debug.Log("Incorrect answer.");
            _audioSourceWrong.Play();
            Debug.Log($"Correct answer: {question.CorrectAnswer}");
        }
        else if (question.CorrectAnswer == question.Answers[Answer - 1])
        {
            Debug.Log("Correct answer!");
            _audioSourceCorrect.Play();
            Score++;
        }
        else if (question.CorrectAnswer != question.Answers[Answer - 1])
        {
            Debug.Log("Incorrect answer.");
            _audioSourceWrong.Play();
            Debug.Log($"Correct answer: {question.CorrectAnswer}");
        }
    }

    public void ShowNextOptions()
    {
        Debug.Log("Showing next options...");
        _scoreCanvas.gameObject.SetActive(false);
        _nextCanvas.gameObject.SetActive(true);
    }

    public void PlayAgain()
    {
        _nextCanvas.gameObject.SetActive(false);
        _isInstructionsShown = true;
        _currentQuestionIndex = 1;
        Score = 0;
        Answer = 0;
        Start();
    }

    public void NewQuiz()
    {
        _nextCanvas.gameObject.SetActive(false);
        _currentQuestionIndex = 1;
        Score = 0;
        Answer = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}



public class Question
{
    public string Text { get; set; }
    public List<string> Answers { get; set; }
    public string CorrectAnswer { get; set; }

    public Question(string text, List<string> answers, string correctAnswer)
    {
        Text = text;
        Answers = answers;
        CorrectAnswer = correctAnswer;
    }
}