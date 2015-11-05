namespace AvitoRuslanParser
{
  class ParsingState
  {
    private bool running;
    private bool pausing;
    private bool paused;
    private bool stopping;
    private bool stopped;
    private bool needAppClose = false;
    private bool automaticMode = false;

    public bool Running
    {
      get { return running; }
      set
      {
        Disable();
        running = value;
        SoftReset();
      }
    }
    public bool Pausing
    {
      get { return pausing; }
      set
      {
        Disable();
        pausing = value;
        SoftReset();
      }
    }
    public bool Paused
    {
      get { return paused; }
      set
      {
        Disable();
        paused = value;
        SoftReset();
      }
    }
    public bool Stopping
    {
      get { return stopping; }
      set
      {
        Disable();
        stopping = value;
        SoftReset();
      }
    }
    public bool Stopped
    {
      get { return stopped; }
      set
      {
        Disable();
        stopped = value;
        SoftReset();
      }
    }
    public bool NeedAppClose
    {
      get { return needAppClose; }
      set { needAppClose = value; }
    }
    public bool AutomaticMode
    {
      get { return automaticMode; }
      set { automaticMode = value; }
    }

    public void SetRunning(bool value = true)
    {
      Running = value;
    }
    public void SetPausing(bool value = true)
    {
      Pausing = value;
    }
    public void SetPaused(bool value = true)
    {
      Paused = value;
    }
    public void SetStopping(bool value = true)
    {
      Stopping = value;
    }
    public void SetStopped(bool value = true)
    {
      Stopped = value;
    }
    public void SetNeedAppClose(bool value = true)
    {
      needAppClose = value;
    }
    public void SetAutomaticMode(bool value = true)
    {
      automaticMode = value;
    }

    public ParsingState()
    {
      Disable();
      SoftReset();
    }
    private void Disable()
    {
      running = false;
      pausing = false;
      paused = false;
      stopping = false;
      stopped = false;
    }
    private void SoftReset()
    {
      if (!running && !pausing && !paused && !stopping && !stopped)
        stopped = true;
    }
  }
}
