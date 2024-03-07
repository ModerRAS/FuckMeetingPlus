using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Input;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using FuckMeetingPlus.Utils;
using Timer = System.Timers.Timer;
using Microsoft.Extensions.Hosting;
using Coravel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FuckMeetingPlus;

public class MainViewModel : ObservableObject
{
    #region Properties

    private string _time;
    private string _meetingId;
    private string _missionText;
    private string _currentTime;
    private Timer _myTimer;
    private bool _isStart { get; set; }
    public string Time
    {
        get => _time;
        set => SetProperty(ref _time, value);
    }
    public string Password { get; set; }


    public string MeetingId
    {
        get => _meetingId;
        set => SetProperty(ref _meetingId, value);
    }

    public string MissionText
    {
        get => _missionText;
        set => SetProperty(ref _missionText, value);
    }

    #endregion

    #region Commands

    public ICommand SaveSettingsCommand { get; }

    private void SaveUserSettings()
    {
        UserSettings.Default.MeetingId = MeetingId;
        UserSettings.Default.Time = Time;

        UserSettings.Default.Save();
    }

    public ICommand StartCommand { get; }

    private void StartFishTouching()
    {
        _isStart = true;
        MissionText = "开始任务";
    }

    #endregion

    public MainViewModel()
    {
        Time = UserSettings.Default.Time;
        MeetingId = UserSettings.Default.MeetingId;
        MissionText = "准备就绪";

        InitializeTimer();

        SaveSettingsCommand = new RelayCommand(SaveUserSettings);
        StartCommand = new RelayCommand(StartFishTouching);
    }

    public static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
                .ConfigureServices(services => {
                    services.AddScheduler();
                }).ConfigureLogging(logging => {
                    logging.ClearProviders();
                    logging.AddSimpleConsole(options => {
                        options.IncludeScopes = true;
                        options.SingleLine = true;
                        options.TimestampFormat = "[yyyy/MM/dd HH:mm:ss] ";
                    });
                });
    /// <summary>
    /// 初始化Timer
    /// </summary>
    private void InitializeTimer()
    {
        IHost host = CreateHostBuilder().Build();
        host.Services.UseScheduler(scheduler => {
            // Easy peasy 👇
            scheduler
                .Schedule(() => {
                    if (!_isStart) {
                        return;
                    }
                    var currentTime = DateTime.Now;
                    if (string.IsNullOrEmpty(Time)) {
                        return;
                    }
                    var setTime = DateTime.ParseExact(Time, "yyyy/MM/dd/HH/mm", null);
                    if (currentTime < setTime) {
                        return;
                    }
                    _isStart = false;
                    // try
                    // {
                    //     Process.Start(Path);
                    // }
                    // catch (Exception exception)
                    // {
                    //     Console.WriteLine(exception);
                    //     throw;
                    // }
                    //
                    // Thread.Sleep(Convert.ToInt32(Waiting) * 1000);
                    //
                    // var intX1 = Convert.ToInt32(X1);
                    // var intY1 = Convert.ToInt32(Y1);
                    // NativeMethod.LeftMouseClick(intX1, intY1);
                    // Thread.Sleep(500);
                    //
                    // NativeMethod.KeyInput(MeetingId);
                    // Thread.Sleep(500);
                    //
                    // var intX2 = Convert.ToInt32(X2);
                    // var intY2 = Convert.ToInt32(Y2);
                    // NativeMethod.LeftMouseClick(intX2, intY2);

                    if (MissionText == "任务完成") {
                        MissionText = "开始任务";
                    }

                    try {
                        if (string.IsNullOrEmpty(Password)) {
                            TencentMeetingUtil.JoinMeeting(MeetingId);
                        } else {
                            TencentMeetingUtil.JoinMeeting(MeetingId, Password);
                        }
                        MissionText = "任务完成";
                    } catch (Exception exception) {
                        Console.WriteLine(exception);
                        throw;
                    }
                    //_isStart = false;

                })
                .EveryFifteenSeconds();
        });
        host.RunAsync();
    }
}