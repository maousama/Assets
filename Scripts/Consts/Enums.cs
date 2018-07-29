/// <summary>
/// UIPanel类型
/// </summary>
public enum WindowType
{
    /// <summary>
    /// 未定义
    /// </summary>
    Undefine,
    /// <summary>
    /// 自由窗体
    /// </summary>
    FreeWindow,
    /// <summary>
    /// 固定窗体
    /// </summary>    
    FixedWindow,
    /// <summary>
    /// 弹窗
    /// </summary>
    MessageBox,
}

/// <summary>
/// UI位置类型
/// </summary>
public enum UIPositionType
{
    LeftUp,
    LeftCenter,
    LeftDown,
    Up,
    Center,
    Down,
    RightUp,
    RightCenter,
    RightDown,
}

public enum CharacterType
{
    Hero,
    Monster,
}
public enum SoundType
{
    _3DSound,
    _2DSound,
}


public enum RangeCheckType
{
    Cycle,
    BoxCast,
    Sector,
}

public enum StrategyType
{
    LockBuildingStrategy,
    LockPlayerStrategy,
}

public enum BuildingType
{
    UnDifine,
    Pillar,
    Chest,
    Crate,
    BonFire,
    PoisonCauldron,

}

