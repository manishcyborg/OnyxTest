class Logger
{
    private readonly StreamWriter _writer;
    public Logger(string path)
    {
        _writer = new StreamWriter(File.Open(path, FileMode.Append))
        {
            AutoFlush = true
        };

        Log("Logger initialized");
    }

    public void Log(string str)
    {
        _writer.WriteLine(string.Format("[{0:dd.MM.yy HH:mm:ss}] {1}", DateTime.Now, str));
    }
}

2 a. Refactor the class so that it can be unit-tested in isolation (independent of external input/output, like file system and current time).
2 b. Write a unit test that asserts that the Logger.Log method prefixes the input string with date and time (avoiding external input/output if possible).

******Solution 2 a

class Logger
{
    private readonly ITimeProvider _timeProvider;
    private readonly IStreamWriter _writer;

    public Logger(ITimeProvider timeProvider, IStreamWriter writer)
    {
        _timeProvider = timeProvider;
        _writer = writer;

        Log("Logger initialized");
    }

    public void Log(string str)
    {
        var message = string.Format("[{0:dd.MM.yy HH:mm:ss}] {1}", _timeProvider.GetCurrentTime(), str);
        _writer.WriteLine(message);
    }
}

interface ITimeProvider
{
    DateTime GetCurrentTime();
}

interface IStreamWriter
{
    void WriteLine(string message);
}


*****Solution 2 b

public class LoggerTests
{
    
    public void Log_PrefixesInputStringWithDateAndTime()
    {
        
        var timeProvider = new MockTimeProvider();
        var writer = new MockStreamWriter();
        var logger = new Logger(timeProvider, writer);

        
        logger.Log("Test message");

        
        var expectedMessage = string.Format("[{0:dd.MM.yy HH:mm:ss}] Test message", timeProvider.GetCurrentTime());
        Assert.AreEqual(expectedMessage, writer.WrittenMessage);
    }
}

class MockTimeProvider : ITimeProvider
{
    public DateTime GetCurrentTime()
    {
        return new DateTime(2020, 1, 1, 12, 0, 0);
    }
}

class MockStreamWriter : IStreamWriter
{
    public string WrittenMessage { get; private set; }

    public void WriteLine(string message)
    {
        WrittenMessage = message;
    }
}