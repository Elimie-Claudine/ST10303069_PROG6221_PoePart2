using NAudio.Wave;

namespace PROG6221_PoePart2_ST10303069
{
    internal class Program
    {
        static string rememberTopic = "";
        static String name = "";
        static string favoriteTopic = "";
        static Random random = new Random();
        static void Main(string[] args)
        {
            Console.Title = "Cybersecurity Awareness Chatbot - SecuBot";

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Launching SecuBot");
            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(400);
                Console.Write(".");
            }

            Thread.Sleep(600);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"
  ╔═══════════════════════════════════════╗
  ║          Welcome to SecuBot -         ║
  ║  Your Cybersecurity Awareness Buddy   ║
  ╠═══════════════════════════════════════╣
  ║                                       ║
  ║           [^  ⌣  ^]    (^_^)           ║
  ║            \____/     /___\           ║
  ║           /     \    |     |          ║
  ║          |SecuBot    ||You||          ║
  ║           \_____/     \___/           ║
  ║                                       ║
  ╚═══════════════════════════════════════╝
");

            Console.ResetColor();


            try
            {
                using (AudioFileReader audioFile = new AudioFileReader("greetings.wav"))
                using (WaveOutEvent outputDevice = new WaveOutEvent())
                {
                    outputDevice.Init(audioFile);
                    outputDevice.Play();
                    while (outputDevice.PlaybackState == PlaybackState.Playing)
                    {
                        Thread.Sleep(100);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error playing audio: " + e.Message);
            }

            Console.WriteLine("Before we start, enter your name please >> ");
            name = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(@"
          _________________________________");
            Console.WriteLine(@"
        <  Hi, " + name + "!                >");
            Console.WriteLine(@" 
        <  Nice meeting you, I'm SecuBot.     >
        <  I will be your guide            >
        <  through this cybersecurity journey.    >
        <  Just ask if you need help ! >
         ---------------------------------
         \     
          \    [SecuBot_2025]
           \   \_/
            .-(_)-.
           | 0   0 |
           |   ^   | 
           |  '-'  |
           +-------+
        ");
            Console.ResetColor();

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine();
                ColoredText("╭───────────────────────────────────────────────╮", ConsoleColor.Green);
                ColoredText("│ Ask your cybersecurity question below         │", ConsoleColor.Green);
                ColoredText("╰───────────────────────────────────────────────╯", ConsoleColor.Green);
                ColoredText("Type 'exit' to close SecuBot at any time.", ConsoleColor.DarkGray);
                Console.Write("You : ");
                string input = Console.ReadLine();


                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.Write("Secubot : "); ColoredText("Oops ! You didn't type anything. Please try again.", ConsoleColor.Red);
                    continue;
                }
                input = input.ToLower();

                if (input.Contains("exit"))
                {
                    Console.Write("Secubot : "); TypeEffect("Thank you for chatting with me, " + name + ". Goodbye!", ConsoleColor.Magenta);
                    break;
                }

                if (input.Contains("thank"))
                {
                    Console.Write("Secubot : "); TypeEffect("Don't mention it, I'm just doing my job! ", ConsoleColor.Cyan);
                    continue;
                }

                if (input.Contains("i'm interested in"))
                {
                    if (input.Contains("privacy"))
                    {
                        ShowTopic("privacy", "That's great to know ! I'll remember that you're interested in privacy.");
                        continue;
                    }
                    else if (input.Contains("browsing"))
                    {
                        ShowTopic("browsing", "Wow that's great ! I'll remember about that.");
                        continue;
                    }
                    else if (input.Contains("scam") || input.Contains("scams"))
                    {
                        ShowTopic("scams", "Great ! I'll remember that you're interested in scams.");
                        continue;
                    }
                    else
                    {
                        Console.Write("Secubot : "); TypeEffect("Sorry, I can't memorize and recall on this topic yet. Thank you for your interest.", ConsoleColor.DarkYellow);
                        continue;
                    }
                }

                Responses(input);
                ContinueConversation(input);

                if (!string.IsNullOrEmpty(favoriteTopic))
                {
                    TypeEffect("Since you're interested in " + favoriteTopic + ", you might want to strengthen your data safety. Don't hesitate to reach out to me and I'll try to help you !", ConsoleColor.DarkYellow);
                }
            }
        }


        static void ShowTopic(string topic, string message)
        {
            switch (topic)
            {
                case "privacy":
                    break;
                case "browsing":
                    break;
                case "scams":
                    break;
            }
            rememberTopic = topic;
            favoriteTopic = topic;
            Console.Write("Secubot : "); TypeEffect(message, ConsoleColor.Yellow);
        }


        static bool Sentiment(string input)
        {
            input = input.ToLower();

            if (input.Contains("worried"))
            {
                if (input.Contains("scam") || input.Contains("scams"))
                {
                    ShowTopic("scams", "It's okay to be worried about scams — they’re very common, but I can teach you how to spot and avoid them!");
                    return true;
                }
                else if (input.Contains("privacy"))
                {
                    ShowTopic("privacy", "Worrying about your privacy is valid. Let’s talk about ways to protect your personal information.");
                    return true;
                }
                else if (input.Contains("browsing") || input.Contains("internet"))
                {
                    ShowTopic("browsing", "Scared of unsafe browsing? Don’t worry, I can help you browse the internet more securely.");
                    return true;
                }
                else
                {
                    Console.Write("Secubot : "); TypeEffect("It's completely understandable to feel that way. Cyber threats can be scary, but I'm here to help you stay safe.", ConsoleColor.Yellow);
                    return true;
                }
            }

            else if (input.Contains("curious"))
            {
                Console.Write("Secubot : "); TypeEffect("I'm glad you're curious! Cybersecurity awareness is the first step to staying protected online.", ConsoleColor.Yellow);
                return true;
            }
            else if (input.Contains("frustrated"))
            {
                Console.Write("Secubot : "); TypeEffect("I get it, this stuff can be confusing sometimes. But I will help you.", ConsoleColor.Yellow);
                return true;
            }

            return false;
        }





        static void Responses(string input)
        {
            string input2 = input.ToLower();
            bool validInput = false;



            bool isFollowUp = input2.Contains("more") || input2.Contains("confused") || input2.Contains("explain");
            if (isFollowUp && !string.IsNullOrEmpty(rememberTopic))
            {
                ContinueConversation(input2);
                return;
            }

            if (input2.Contains("phishing") && input2.Contains("tip"))
            {
                rememberTopic = "phishing";
                validInput = true;
            }
            else if (Sentiment(input))
            {
                validInput = true;
            }
            else if (input2.Contains("browsing") && input2.Contains("tip"))
            {
                rememberTopic = "browsing";
                validInput = true;
            }
            else if (input2.Contains("privacy") && input2.Contains("tip"))
            {
                rememberTopic = "privacy";
                validInput = true;
            }
            else if (input2.Contains("scams") && input2.Contains("tip"))
            {
                rememberTopic = "scams";
                validInput = true;
            }
            else
            {
                Dictionary<string, string> responses = new Dictionary<string, string>()
        {
        { "how are you", "I'm doing great, and I hope you are too! Let me know if you need advice to stay protected online." },
        { "your purpose", "My role is to teach you how to stay safe online." },
        { "help", "You can ask about topics like phishing, privacy, scams, safe browsing, passwords, firewalls, encryption, and more!" },
        { "ask you", "You can ask me anything related to online safety, such as passwords, browsing, privacy, etc." },
        { "password", "Use strong passwords with a mix of uppercase/lowercase letters, numbers, and symbols. Avoid reusing them across accounts, and consider using a password manager." },
        { "firewall", "A firewall acts like a barrier that blocks unauthorized access to your network or computer while allowing safe communication." },
        { "encryption", "Encryption turns your data into unreadable code so only authorized parties with the right key can understand it. It's essential for data privacy." }
            };


                foreach (var entry in responses)
                {
                    if (input2.Contains(entry.Key))
                    {
                        Console.Write("Secubot : "); TypeEffect(entry.Value, ConsoleColor.Cyan);
                        validInput = true;
                        break;
                    }
                }
            }

            if (!validInput)
            {
                Console.Write("Secubot : "); ColoredText("I didn't quite understand that. Please, rephrase.", ConsoleColor.Red);
            }
        }


        static void ContinueConversation(string input)
        {
            bool isConfused = input.Contains("confused") || input.Contains("explain") || input.Contains("more");

            switch (rememberTopic)
            {
                case "phishing":
                    if (isConfused)
                    {
                        Console.Write("Secubot : "); TypeEffect("Phishing is when scammers trick you into giving personal info like passwords by using fake emails or websites that look real. Always think before you click !", ConsoleColor.Blue);
                    }
                    else
                    {
                        TipPhishing();
                    }
                    break;
                case "browsing":
                    if (isConfused)
                    {
                        Console.Write("Secubot : "); TypeEffect("Safe browsing means using HTTPS sites, avoiding shady links, not clicking pop-ups, and keeping your browser and antivirus updated — all to protect your data and privacy online.", ConsoleColor.Blue);
                    }
                    else
                    {
                        TipBrowsing();
                    }
                    break;
                case "privacy":
                    if (isConfused)
                    {
                        Console.Write("Secubot : "); TypeEffect("Privacy means controlling who sees your personal info, limiting app permissions, adjusting social media settings, and thinking twice before sharing anything online.", ConsoleColor.Blue);
                    }
                    else
                    {
                        TipPrivacy();
                    }
                    break;
                case "scams":
                    if (isConfused)
                    {
                        Console.Write("Secubot : "); TypeEffect("Scams try to trick you into giving money or info by pretending to be someone you trust. Always verify messages, avoid clicking unknown links, and beware of deals that seem too good to be true.", ConsoleColor.Blue);
                    }
                    else
                    {
                        TipScam();
                    }

                    break;


            }
        }


        static void TipPhishing()
        {
            string[] tips = {
        "If an email asks for your personal info out of the blue, it's probably a scam : better to be safe and double-check.",
        "Before clicking a link, hover over it with your mouse. Make sure it leads where it says it does.",
        "Avoid opening attachments from people you don’t know.. they might look harmless but could contain something dangerous.",
        "Scammers often fake email addresses to look real. Take a second to check if it feels off."
    };
            Console.Write("SecuBot: "); TypeEffect(tips[random.Next(tips.Length)], ConsoleColor.Cyan);
        }

        static void TipBrowsing()
        {
            string[] tips = {
        "Stick to secure websites — look for 'HTTPS' in the address bar, especially when entering personal info.",
        "If a link looks sketchy or unfamiliar, it's better not to click. Trust your instincts online.",
        "Make sure your browser and antivirus are always up to date — it helps keep sneaky threats away.",
        "Clearing your cookies and browsing history now and then keeps things tidy and helps protect your privacy."
    };
            Console.Write("SecuBot: "); TypeEffect(tips[random.Next(tips.Length)], ConsoleColor.Cyan);
        }

        static void TipPrivacy()
        {
            string[] tips = {
        "Take a minute to check your app and account privacy settings. You'd be surprised what’s public by default.",
        "Think twice before sharing your phone number or address online — once it’s out there, it’s hard to take back.",
        "Only give apps the permissions they truly need. No need for a flashlight app to access your contacts!",
        "Using incognito mode can be handy when you don’t want your browser to remember where you’ve been."
    };
            Console.Write("SecuBot: "); TypeEffect(tips[random.Next(tips.Length)], ConsoleColor.Cyan);
        }

        static void TipScam()
        {
            string[] tips = {
        "If something feels off, pause — don’t rush to give out info or money. Scammers count on panic.",
        "Got a weird message or call? Take a second to check if it might be fake... better safe than sorry.",
        "If you think you've been scammed, report it and call your bank right away. The faster, the better.",
        "If a deal seems way too good to be true, it probably is — trust your gut."
    };
            Console.Write("SecuBot: "); TypeEffect(tips[random.Next(tips.Length)], ConsoleColor.Cyan);
        }


        static void TypeEffect(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(15);
            }
            Console.WriteLine();
            Console.ResetColor();
        }
        static void ColoredText(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
