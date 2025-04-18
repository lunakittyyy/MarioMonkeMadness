public static class SM64Constants
{
    // seq_ids.h
    const ushort SEQ_VARIATION = 0x80;
    enum SeqId : ushort
    {
        SEQ_SOUND_PLAYER,                 // 0x00
        SEQ_EVENT_CUTSCENE_COLLECT_STAR,  // 0x01
        SEQ_MENU_TITLE_SCREEN,            // 0x02
        SEQ_LEVEL_GRASS,                  // 0x03
        SEQ_LEVEL_INSIDE_CASTLE,          // 0x04
        SEQ_LEVEL_WATER,                  // 0x05
        SEQ_LEVEL_HOT,                    // 0x06
        SEQ_LEVEL_BOSS_KOOPA,             // 0x07
        SEQ_LEVEL_SNOW,                   // 0x08
        SEQ_LEVEL_SLIDE,                  // 0x09
        SEQ_LEVEL_SPOOKY,                 // 0x0A
        SEQ_EVENT_PIRANHA_PLANT,          // 0x0B
        SEQ_LEVEL_UNDERGROUND,            // 0x0C
        SEQ_MENU_STAR_SELECT,             // 0x0D
        SEQ_EVENT_POWERUP,                // 0x0E
        SEQ_EVENT_METAL_CAP,              // 0x0F
        SEQ_EVENT_KOOPA_MESSAGE,          // 0x10
        SEQ_LEVEL_KOOPA_ROAD,             // 0x11
        SEQ_EVENT_HIGH_SCORE,             // 0x12
        SEQ_EVENT_MERRY_GO_ROUND,         // 0x13
        SEQ_EVENT_RACE,                   // 0x14
        SEQ_EVENT_CUTSCENE_STAR_SPAWN,    // 0x15
        SEQ_EVENT_BOSS,                   // 0x16
        SEQ_EVENT_CUTSCENE_COLLECT_KEY,   // 0x17
        SEQ_EVENT_ENDLESS_STAIRS,         // 0x18
        SEQ_LEVEL_BOSS_KOOPA_FINAL,       // 0x19
        SEQ_EVENT_CUTSCENE_CREDITS,       // 0x1A
        SEQ_EVENT_SOLVE_PUZZLE,           // 0x1B
        SEQ_EVENT_TOAD_MESSAGE,           // 0x1C
        SEQ_EVENT_PEACH_MESSAGE,          // 0x1D
        SEQ_EVENT_CUTSCENE_INTRO,         // 0x1E
        SEQ_EVENT_CUTSCENE_VICTORY,       // 0x1F
        SEQ_EVENT_CUTSCENE_ENDING,        // 0x20
        SEQ_MENU_FILE_SELECT,             // 0x21
        SEQ_EVENT_CUTSCENE_LAKITU,        // 0x22 (not in JP)
        SEQ_COUNT
    };


    // audio_defines.h
    enum SoundStatus : uint
    {
        SOUND_STATUS_STOPPED=0,
        SOUND_STATUS_STARTING=1,
        SOUND_STATUS_WAITING=SOUND_STATUS_STARTING,
        SOUND_STATUS_PLAYING=2
    }
    static uint SOUND_ARG_LOAD(uint bank, uint playFlags, uint soundID, uint priority, uint flags2)
    {
        return (bank << 28) |
            (playFlags << 24) | (soundID << 16) | (priority << 8) |
            (flags2 << 4) | (uint)SoundStatus.SOUND_STATUS_STARTING;
    }
    public static readonly uint SOUND_ACTION_TERRAIN_JUMP               = SOUND_ARG_LOAD(0, 4, 0x00, 0x80, 8);
    public static readonly uint SOUND_ACTION_TERRAIN_LANDING            = SOUND_ARG_LOAD(0, 4, 0x08, 0x80, 8);
    public static readonly uint SOUND_ACTION_TERRAIN_STEP               = SOUND_ARG_LOAD(0, 6, 0x10, 0x80, 8);
    public static readonly uint SOUND_ACTION_TERRAIN_BODY_HIT_GROUND    = SOUND_ARG_LOAD(0, 4, 0x18, 0x80, 8);
    public static readonly uint SOUND_ACTION_TERRAIN_STEP_TIPTOE        = SOUND_ARG_LOAD(0, 6, 0x20, 0x80, 8);
    public static readonly uint SOUND_ACTION_TERRAIN_STUCK_IN_GROUND    = SOUND_ARG_LOAD(0, 4, 0x48, 0x80, 8);
    public static readonly uint SOUND_ACTION_TERRAIN_HEAVY_LANDING      = SOUND_ARG_LOAD(0, 4, 0x60, 0x80, 8);

    public static readonly uint SOUND_ACTION_METAL_JUMP                         = SOUND_ARG_LOAD(0, 4, 0x28, 0x90, 8);
    public static readonly uint SOUND_ACTION_METAL_LANDING                      = SOUND_ARG_LOAD(0, 4, 0x29, 0x90, 8);
    public static readonly uint SOUND_ACTION_METAL_STEP                         = SOUND_ARG_LOAD(0, 4, 0x2A, 0x90, 8);
    public static readonly uint SOUND_ACTION_METAL_HEAVY_LANDING                = SOUND_ARG_LOAD(0, 4, 0x2B, 0x90, 8);
    public static readonly uint SOUND_ACTION_CLAP_HANDS_COLD                    = SOUND_ARG_LOAD(0, 6, 0x2C, 0x00, 8);
    public static readonly uint SOUND_ACTION_HANGING_STEP                       = SOUND_ARG_LOAD(0, 4, 0x2D, 0xA0, 8);
    public static readonly uint SOUND_ACTION_QUICKSAND_STEP                     = SOUND_ARG_LOAD(0, 4, 0x2E, 0x00, 8);
    public static readonly uint SOUND_ACTION_METAL_STEP_TIPTOE                  = SOUND_ARG_LOAD(0, 4, 0x2F, 0x90, 8);
    /* not verified */ public static readonly uint SOUND_ACTION_UNKNOWN430      = SOUND_ARG_LOAD(0, 4, 0x30, 0xC0, 8);
    /* not verified */ public static readonly uint SOUND_ACTION_UNKNOWN431      = SOUND_ARG_LOAD(0, 4, 0x31, 0x60, 8);
    /* not verified */ public static readonly uint SOUND_ACTION_UNKNOWN432      = SOUND_ARG_LOAD(0, 4, 0x32, 0x80, 8);
    public static readonly uint SOUND_ACTION_SWIM                               = SOUND_ARG_LOAD(0, 4, 0x33, 0x80, 8);
    /* not verified */ public static readonly uint SOUND_ACTION_UNKNOWN434      = SOUND_ARG_LOAD(0, 4, 0x34, 0x80, 8);
    public static readonly uint SOUND_ACTION_THROW                              = SOUND_ARG_LOAD(0, 4, 0x35, 0x80, 8);
    public static readonly uint SOUND_ACTION_KEY_SWISH                          = SOUND_ARG_LOAD(0, 4, 0x36, 0x80, 8);
    public static readonly uint SOUND_ACTION_SPIN                               = SOUND_ARG_LOAD(0, 4, 0x37, 0x80, 8);
    public static readonly uint SOUND_ACTION_TWIRL                              = SOUND_ARG_LOAD(0, 4, 0x38, 0x80, 8); // same sound as spin
    /* not verified */ public static readonly uint SOUND_ACTION_CLIMB_UP_TREE   = SOUND_ARG_LOAD(0, 4, 0x3A, 0x80, 8);
    /* not verified */ public static readonly uint SOUND_ACTION_CLIMB_DOWN_TREE = 0x003;
    /* not verified */ public static readonly uint SOUND_ACTION_UNK3C           = 0x003;
    /* not verified */ public static readonly uint SOUND_ACTION_UNKNOWN43D      = SOUND_ARG_LOAD(0, 4, 0x3D, 0x80, 8);
    /* not verified */ public static readonly uint SOUND_ACTION_UNKNOWN43E      = SOUND_ARG_LOAD(0, 4, 0x3E, 0x80, 8);
    /* not verified */ public static readonly uint SOUND_ACTION_PAT_BACK        = SOUND_ARG_LOAD(0, 4, 0x3F, 0x80, 8);
    public static readonly uint SOUND_ACTION_BRUSH_HAIR                         = SOUND_ARG_LOAD(0, 4, 0x40, 0x80, 8);
    /* not verified */ public static readonly uint SOUND_ACTION_CLIMB_UP_POLE   = SOUND_ARG_LOAD(0, 4, 0x41, 0x80, 8);
    public static readonly uint SOUND_ACTION_METAL_BONK                         = SOUND_ARG_LOAD(0, 4, 0x42, 0x80, 8);
    public static readonly uint SOUND_ACTION_UNSTUCK_FROM_GROUND                = SOUND_ARG_LOAD(0, 4, 0x43, 0x80, 8);
    /* not verified */ public static readonly uint SOUND_ACTION_HIT             = SOUND_ARG_LOAD(0, 4, 0x44, 0xC0, 8);
    /* not verified */ public static readonly uint SOUND_ACTION_HIT_2           = SOUND_ARG_LOAD(0, 4, 0x44, 0xB0, 8);
    /* not verified */ public static readonly uint SOUND_ACTION_HIT_3           = SOUND_ARG_LOAD(0, 4, 0x44, 0xA0, 8);
    public static readonly uint SOUND_ACTION_BONK                               = SOUND_ARG_LOAD(0, 4, 0x45, 0xA0, 8);
    public static readonly uint SOUND_ACTION_SHRINK_INTO_BBH                    = SOUND_ARG_LOAD(0, 4, 0x46, 0xA0, 8);
    public static readonly uint SOUND_ACTION_SWIM_FAST                          = SOUND_ARG_LOAD(0, 4, 0x47, 0xA0, 8);
    public static readonly uint SOUND_ACTION_METAL_JUMP_WATER                   = SOUND_ARG_LOAD(0, 4, 0x50, 0x90, 8);
    public static readonly uint SOUND_ACTION_METAL_LAND_WATER                   = SOUND_ARG_LOAD(0, 4, 0x51, 0x90, 8);
    public static readonly uint SOUND_ACTION_METAL_STEP_WATER                   = SOUND_ARG_LOAD(0, 4, 0x52, 0x90, 8);
    /* not verified */ public static readonly uint SOUND_ACTION_UNK53           = 0x005;
    /* not verified */ public static readonly uint SOUND_ACTION_UNK54           = 0x005;
    /* not verified */ public static readonly uint SOUND_ACTION_UNK55           = 0x005;
    /* not verified */ public static readonly uint SOUND_ACTION_FLYING_FAST     = SOUND_ARG_LOAD(0, 4, 0x56, 0x80, 8); // "swoop"?
    public static readonly uint SOUND_ACTION_TELEPORT                           = SOUND_ARG_LOAD(0, 4, 0x57, 0xC0, 8);
    /* not verified */ public static readonly uint SOUND_ACTION_UNKNOWN458      = SOUND_ARG_LOAD(0, 4, 0x58, 0xA0, 8);
    /* not verified */ public static readonly uint SOUND_ACTION_BOUNCE_OFF_OBJECT   = SOUND_ARG_LOAD(0, 4, 0x59, 0xB0, 8);
    /* not verified */ public static readonly uint SOUND_ACTION_SIDE_FLIP_UNK   = SOUND_ARG_LOAD(0, 4, 0x5A, 0x80, 8);
    public static readonly uint SOUND_ACTION_READ_SIGN                          = SOUND_ARG_LOAD(0, 4, 0x5B, 0xFF, 8);
    /* not verified */ public static readonly uint SOUND_ACTION_UNKNOWN45C      = SOUND_ARG_LOAD(0, 4, 0x5C, 0x80, 8);
    /* not verified */ public static readonly uint SOUND_ACTION_UNK5D           = 0x005;
    /* not verified */ public static readonly uint SOUND_ACTION_INTRO_UNK45E    = SOUND_ARG_LOAD(0, 4, 0x5E, 0x80, 8);
    /* not verified */ public static readonly uint SOUND_ACTION_INTRO_UNK45F    = SOUND_ARG_LOAD(0, 4, 0x5F, 0x80, 8);

    /* Moving Sound Effects */

    // Terrain-dependent moving sounds; a value 0-7 is added to the sound ID before
    // playing. See higher up for the different terrain types.
    public static readonly uint SOUND_MOVING_TERRAIN_SLIDE              = SOUND_ARG_LOAD(1, 4, 0x00, 0x00, 0);
    public static readonly uint SOUND_MOVING_TERRAIN_RIDING_SHELL       = SOUND_ARG_LOAD(1, 4, 0x20, 0x00, 0);

    public static readonly uint SOUND_MOVING_LAVA_BURN                  = SOUND_ARG_LOAD(1, 4, 0x10, 0x00, 0); // ?
    public static readonly uint SOUND_MOVING_SLIDE_DOWN_POLE            = SOUND_ARG_LOAD(1, 4, 0x11, 0x00, 0); // ?
    public static readonly uint SOUND_MOVING_SLIDE_DOWN_TREE            = SOUND_ARG_LOAD(1, 4, 0x12, 0x80, 0);
    public static readonly uint SOUND_MOVING_QUICKSAND_DEATH            = SOUND_ARG_LOAD(1, 4, 0x14, 0x00, 0);
    public static readonly uint SOUND_MOVING_SHOCKED                    = SOUND_ARG_LOAD(1, 4, 0x16, 0x00, 0);
    public static readonly uint SOUND_MOVING_FLYING                     = SOUND_ARG_LOAD(1, 4, 0x17, 0x00, 0);
    public static readonly uint SOUND_MOVING_ALMOST_DROWNING            = SOUND_ARG_LOAD(1, 0xC, 0x18, 0x00, 0);
    public static readonly uint SOUND_MOVING_AIM_CANNON                 = SOUND_ARG_LOAD(1, 0xD, 0x19, 0x20, 0);
    public static readonly uint SOUND_MOVING_UNK1A                      = 0x101A;
    public static readonly uint SOUND_MOVING_RIDING_SHELL_LAVA          = SOUND_ARG_LOAD(1, 4, 0x28, 0x00, 0);

    /* Mario Sound Effects */
    // A random number 0-2 is added to the sound ID before playing, producing Yah/Wah/Hoo
    public static readonly uint SOUND_MARIO_YAH_WAH_HOO                         = SOUND_ARG_LOAD(2, 4, 0x00, 0x80, 8);
    /* not verified */ public static readonly uint SOUND_MARIO_HOOHOO           = SOUND_ARG_LOAD(2, 4, 0x03, 0x80, 8);
    /* not verified */ public static readonly uint SOUND_MARIO_YAHOO            = SOUND_ARG_LOAD(2, 4, 0x04, 0x80, 8);
    /* not verified */ public static readonly uint SOUND_MARIO_UH               = SOUND_ARG_LOAD(2, 4, 0x05, 0x80, 8);
    /* not verified */ public static readonly uint SOUND_MARIO_HRMM             = SOUND_ARG_LOAD(2, 4, 0x06, 0x80, 8);
    /* not verified */ public static readonly uint SOUND_MARIO_WAH2             = SOUND_ARG_LOAD(2, 4, 0x07, 0x80, 8);
    /* not verified */ public static readonly uint SOUND_MARIO_WHOA             = SOUND_ARG_LOAD(2, 4, 0x08, 0xC0, 8);
    /* not verified */ public static readonly uint SOUND_MARIO_EEUH             = SOUND_ARG_LOAD(2, 4, 0x09, 0x80, 8);
    /* not verified */ public static readonly uint SOUND_MARIO_ATTACKED         = SOUND_ARG_LOAD(2, 4, 0x0A, 0xFF, 8);
    /* not verified */ public static readonly uint SOUND_MARIO_OOOF             = SOUND_ARG_LOAD(2, 4, 0x0B, 0x80, 8);
    /* not verified */ public static readonly uint SOUND_MARIO_OOOF2            = SOUND_ARG_LOAD(2, 4, 0x0B, 0xD0, 8);
    public static readonly uint SOUND_MARIO_HERE_WE_GO                          = SOUND_ARG_LOAD(2, 4, 0x0C, 0x80, 8);
    /* not verified */ public static readonly uint SOUND_MARIO_YAWNING          = SOUND_ARG_LOAD(2, 4, 0x0D, 0x80, 8);
    public static readonly uint SOUND_MARIO_SNORING1                            = SOUND_ARG_LOAD(2, 4, 0x0E, 0x80, 8);
    public static readonly uint SOUND_MARIO_SNORING2                            = SOUND_ARG_LOAD(2, 4, 0x0F, 0x80, 8);
    /* not verified */ public static readonly uint SOUND_MARIO_WAAAOOOW         = SOUND_ARG_LOAD(2, 4, 0x10, 0xC0, 8);
    /* not verified */ public static readonly uint SOUND_MARIO_HAHA             = SOUND_ARG_LOAD(2, 4, 0x11, 0x80, 8);
    /* not verified */ public static readonly uint SOUND_MARIO_HAHA_2           = SOUND_ARG_LOAD(2, 4, 0x11, 0xF0, 8);
    /* not verified */ public static readonly uint SOUND_MARIO_UH2              = SOUND_ARG_LOAD(2, 4, 0x13, 0xD0, 8);
    /* not verified */ public static readonly uint SOUND_MARIO_UH2_2            = SOUND_ARG_LOAD(2, 4, 0x13, 0x80, 8);
    /* not verified */ public static readonly uint SOUND_MARIO_ON_FIRE          = SOUND_ARG_LOAD(2, 4, 0x14, 0xA0, 8);
    /* not verified */ public static readonly uint SOUND_MARIO_DYING            = SOUND_ARG_LOAD(2, 4, 0x15, 0xFF, 8);
    public static readonly uint SOUND_MARIO_PANTING_COLD                        = SOUND_ARG_LOAD(2, 4, 0x16, 0x80, 8);

    // A random number 0-2 is added to the sound ID before playing
    public static readonly uint SOUND_MARIO_PANTING                     = SOUND_ARG_LOAD(2, 4, 0x18, 0x80, 8);

    public static readonly uint SOUND_MARIO_COUGHING1                   = SOUND_ARG_LOAD(2, 4, 0x1B, 0x80, 8);
    public static readonly uint SOUND_MARIO_COUGHING2                   = SOUND_ARG_LOAD(2, 4, 0x1C, 0x80, 8);
    public static readonly uint SOUND_MARIO_COUGHING3                   = SOUND_ARG_LOAD(2, 4, 0x1D, 0x80, 8);
    public static readonly uint SOUND_MARIO_PUNCH_YAH                   = SOUND_ARG_LOAD(2, 4, 0x1E, 0x80, 8);
    public static readonly uint SOUND_MARIO_PUNCH_HOO                   = SOUND_ARG_LOAD(2, 4, 0x1F, 0x80, 8);
    public static readonly uint SOUND_MARIO_MAMA_MIA                    = SOUND_ARG_LOAD(2, 4, 0x20, 0x80, 8);
    public static readonly uint SOUND_MARIO_OKEY_DOKEY                  = 0x202;
    public static readonly uint SOUND_MARIO_GROUND_POUND_WAH            = SOUND_ARG_LOAD(2, 4, 0x22, 0x80, 8);
    public static readonly uint SOUND_MARIO_DROWNING                    = SOUND_ARG_LOAD(2, 4, 0x23, 0xF0, 8);
    public static readonly uint SOUND_MARIO_PUNCH_WAH                   = SOUND_ARG_LOAD(2, 4, 0x24, 0x80, 8);

    /* Mario Sound Effects (US/EU only); */
    public static readonly uint SOUND_PEACH_DEAR_MARIO                  = SOUND_ARG_LOAD(2, 4, 0x28, 0xFF, 8);

    // A random number 0-4 is added to the sound ID before playing, producing one of
    // Yahoo! (60% chance);, Waha! (20%);, or Yippee! (20%);.
    public static readonly uint SOUND_MARIO_YAHOO_WAHA_YIPPEE           = SOUND_ARG_LOAD(2, 4, 0x2B, 0x80, 8);

    public static readonly uint SOUND_MARIO_DOH                         = SOUND_ARG_LOAD(2, 4, 0x30, 0x80, 8);
    public static readonly uint SOUND_MARIO_GAME_OVER                   = SOUND_ARG_LOAD(2, 4, 0x31, 0xFF, 8);
    public static readonly uint SOUND_MARIO_HELLO                       = SOUND_ARG_LOAD(2, 4, 0x32, 0xFF, 8);
    public static readonly uint SOUND_MARIO_PRESS_START_TO_PLAY         = SOUND_ARG_LOAD(2, 4, 0x33, 0xFF, 0xA);
    public static readonly uint SOUND_MARIO_TWIRL_BOUNCE                = SOUND_ARG_LOAD(2, 4, 0x34, 0x80, 8);
    public static readonly uint SOUND_MARIO_SNORING3                    = SOUND_ARG_LOAD(2, 4, 0x35, 0xFF, 8);
    public static readonly uint SOUND_MARIO_SO_LONGA_BOWSER             = SOUND_ARG_LOAD(2, 4, 0x36, 0x80, 8);
    public static readonly uint SOUND_MARIO_IMA_TIRED                   = SOUND_ARG_LOAD(2, 4, 0x37, 0x80, 8);

    /* Princess Peach Sound Effects (US/EU only); */
    public static readonly uint SOUND_PEACH_MARIO                       = SOUND_ARG_LOAD(2, 4, 0x38, 0xFF, 8);
    public static readonly uint SOUND_PEACH_POWER_OF_THE_STARS          = SOUND_ARG_LOAD(2, 4, 0x39, 0xFF, 8);
    public static readonly uint SOUND_PEACH_THANKS_TO_YOU               = SOUND_ARG_LOAD(2, 4, 0x3A, 0xFF, 8);
    public static readonly uint SOUND_PEACH_THANK_YOU_MARIO             = SOUND_ARG_LOAD(2, 4, 0x3B, 0xFF, 8);
    public static readonly uint SOUND_PEACH_SOMETHING_SPECIAL           = SOUND_ARG_LOAD(2, 4, 0x3C, 0xFF, 8);
    public static readonly uint SOUND_PEACH_BAKE_A_CAKE                 = SOUND_ARG_LOAD(2, 4, 0x3D, 0xFF, 8);
    public static readonly uint SOUND_PEACH_FOR_MARIO                   = SOUND_ARG_LOAD(2, 4, 0x3E, 0xFF, 8);
    public static readonly uint SOUND_PEACH_MARIO2                      = SOUND_ARG_LOAD(2, 4, 0x3F, 0xFF, 8);

    /* General Sound Effects */
    public static readonly uint SOUND_GENERAL_ACTIVATE_CAP_SWITCH                   = SOUND_ARG_LOAD(3, 0, 0x00, 0x80, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_FLAME_OUT          = SOUND_ARG_LOAD(3, 0, 0x03, 0x80, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_OPEN_WOOD_DOOR     = SOUND_ARG_LOAD(3, 0, 0x04, 0xC0, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_CLOSE_WOOD_DOOR    = SOUND_ARG_LOAD(3, 0, 0x05, 0xC0, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_OPEN_IRON_DOOR     = SOUND_ARG_LOAD(3, 0, 0x06, 0xC0, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_CLOSE_IRON_DOOR    = SOUND_ARG_LOAD(3, 0, 0x07, 0xC0, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_BUBBLES            = 0x300;
    /* not verified */ public static readonly uint SOUND_GENERAL_MOVING_WATER       = SOUND_ARG_LOAD(3, 0, 0x09, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_SWISH_WATER        = SOUND_ARG_LOAD(3, 0, 0x0A, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_QUIET_BUBBLE       = SOUND_ARG_LOAD(3, 0, 0x0B, 0x00, 8);
    public static readonly uint SOUND_GENERAL_VOLCANO_EXPLOSION                     = SOUND_ARG_LOAD(3, 0, 0x0C, 0x80, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_QUIET_BUBBLE2      = SOUND_ARG_LOAD(3, 0, 0x0D, 0x00, 8);
    public static readonly uint SOUND_GENERAL_CASTLE_TRAP_OPEN                      = SOUND_ARG_LOAD(3, 0, 0x0E, 0x80, 8);
    public static readonly uint SOUND_GENERAL_WALL_EXPLOSION                        = SOUND_ARG_LOAD(3, 0, 0x0F, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_COIN               = SOUND_ARG_LOAD(3, 8, 0x11, 0x80, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_COIN_WATER         = SOUND_ARG_LOAD(3, 8, 0x12, 0x80, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_SHORT_STAR         = SOUND_ARG_LOAD(3, 0, 0x16, 0x00, 9);
    /* not verified */ public static readonly uint SOUND_GENERAL_BIG_CLOCK          = SOUND_ARG_LOAD(3, 0, 0x17, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_LOUD_POUND         = 0x3018;
    /* not verified */ public static readonly uint SOUND_GENERAL_LOUD_POUND2        = 0x301;
    /* not verified */ public static readonly uint SOUND_GENERAL_SHORT_POUND1       = 0x301;
    /* not verified */ public static readonly uint SOUND_GENERAL_SHORT_POUND2       = 0x301;
    /* not verified */ public static readonly uint SOUND_GENERAL_SHORT_POUND3       = 0x301;
    /* not verified */ public static readonly uint SOUND_GENERAL_SHORT_POUND4       = 0x301;
    /* not verified */ public static readonly uint SOUND_GENERAL_SHORT_POUND5       = 0x301;
    /* not verified */ public static readonly uint SOUND_GENERAL_SHORT_POUND6       = 0x301;
    public static readonly uint SOUND_GENERAL_OPEN_CHEST                            = SOUND_ARG_LOAD(3, 1, 0x20, 0x80, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_CLAM_SHELL1        = SOUND_ARG_LOAD(3, 1, 0x22, 0x80, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_BOX_LANDING        = SOUND_ARG_LOAD(3, 0, 0x24, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_BOX_LANDING_2      = SOUND_ARG_LOAD(3, 2, 0x24, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_UNKNOWN1           = SOUND_ARG_LOAD(3, 0, 0x25, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_UNKNOWN1_2         = SOUND_ARG_LOAD(3, 2, 0x25, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_CLAM_SHELL2        = SOUND_ARG_LOAD(3, 0, 0x26, 0x40, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_CLAM_SHELL3        = SOUND_ARG_LOAD(3, 0, 0x27, 0x40, 8);
    //#ifdef VERSION_JP
    public static readonly uint SOUND_GENERAL_PAINTING_EJECT                        = SOUND_ARG_LOAD(3, 8, 0x28, 0x00, 8);
    //#else
    //public static readonly uint SOUND_GENERAL_PAINTING_EJECT                        = SOUND_ARG_LOAD(3, 9, 0x28, 0x00, 8);
    //#endif
    public static readonly uint SOUND_GENERAL_LEVEL_SELECT_CHANGE                   = SOUND_ARG_LOAD(3, 0, 0x2B, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_PLATFORM           = SOUND_ARG_LOAD(3, 0, 0x2D, 0x80, 8);
    public static readonly uint SOUND_GENERAL_DONUT_PLATFORM_EXPLOSION              = SOUND_ARG_LOAD(3, 0, 0x2E, 0x20, 8);
    public static readonly uint SOUND_GENERAL_BOWSER_BOMB_EXPLOSION                 = SOUND_ARG_LOAD(3, 1, 0x2F, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_COIN_SPURT         = SOUND_ARG_LOAD(3, 0, 0x30, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_COIN_SPURT_2       = SOUND_ARG_LOAD(3, 8, 0x30, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_COIN_SPURT_EU      = SOUND_ARG_LOAD(3, 8, 0x30, 0x20, 8);

    /* not verified */ public static readonly uint SOUND_GENERAL_EXPLOSION6         = 0x303;
    /* not verified */ public static readonly uint SOUND_GENERAL_UNK32              = 0x303;
    /* not verified */ public static readonly uint SOUND_GENERAL_BOAT_TILT1         = SOUND_ARG_LOAD(3, 0, 0x34, 0x40, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_BOAT_TILT2         = SOUND_ARG_LOAD(3, 0, 0x35, 0x40, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_COIN_DROP          = SOUND_ARG_LOAD(3, 0, 0x36, 0x40, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_UNKNOWN3_LOWPRIO   = SOUND_ARG_LOAD(3, 0, 0x37, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_UNKNOWN3           = SOUND_ARG_LOAD(3, 0, 0x37, 0x80, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_UNKNOWN3_2         = SOUND_ARG_LOAD(3, 8, 0x37, 0x80, 8);
    public static readonly uint SOUND_GENERAL_PENDULUM_SWING                        = SOUND_ARG_LOAD(3, 0, 0x38, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_CHAIN_CHOMP1       = SOUND_ARG_LOAD(3, 0, 0x39, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_CHAIN_CHOMP2       = SOUND_ARG_LOAD(3, 0, 0x3A, 0x00, 8);
    public static readonly uint SOUND_GENERAL_DOOR_TURN_KEY                         = SOUND_ARG_LOAD(3, 0, 0x3B, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_MOVING_IN_SAND     = SOUND_ARG_LOAD(3, 0, 0x3C, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_UNKNOWN4_LOWPRIO   = SOUND_ARG_LOAD(3, 0, 0x3D, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_UNKNOWN4           = SOUND_ARG_LOAD(3, 0, 0x3D, 0x80, 8);
    public static readonly uint SOUND_GENERAL_MOVING_PLATFORM_SWITCH                = SOUND_ARG_LOAD(3, 0, 0x3E, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_CAGE_OPEN          = SOUND_ARG_LOAD(3, 0, 0x3F, 0xA0, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_QUIET_POUND1_LOWPRIO   = SOUND_ARG_LOAD(3, 0, 0x40, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_QUIET_POUND1       = SOUND_ARG_LOAD(3, 0, 0x40, 0x40, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_BREAK_BOX          = SOUND_ARG_LOAD(3, 0, 0x41, 0xC0, 8);
    public static readonly uint SOUND_GENERAL_DOOR_INSERT_KEY                       = SOUND_ARG_LOAD(3, 0, 0x42, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_QUIET_POUND2       = SOUND_ARG_LOAD(3, 0, 0x43, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_BIG_POUND          = SOUND_ARG_LOAD(3, 0, 0x44, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_UNK45              = SOUND_ARG_LOAD(3, 0, 0x45, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_UNK46_LOWPRIO      = SOUND_ARG_LOAD(3, 0, 0x46, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_UNK46              = SOUND_ARG_LOAD(3, 0, 0x46, 0x80, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_CANNON_UP          = SOUND_ARG_LOAD(3, 0, 0x47, 0x80, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_GRINDEL_ROLL       = SOUND_ARG_LOAD(3, 0, 0x48, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_EXPLOSION7         = 0x304;
    /* not verified */ public static readonly uint SOUND_GENERAL_SHAKE_COFFIN       = 0x304;
    /* not verified */ public static readonly uint SOUND_GENERAL_RACE_GUN_SHOT      = SOUND_ARG_LOAD(3, 1, 0x4D, 0x40, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_STAR_DOOR_OPEN     = SOUND_ARG_LOAD(3, 0, 0x4E, 0xC0, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_STAR_DOOR_CLOSE    = SOUND_ARG_LOAD(3, 0, 0x4F, 0xC0, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_POUND_ROCK         = SOUND_ARG_LOAD(3, 0, 0x56, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_STAR_APPEARS       = SOUND_ARG_LOAD(3, 0, 0x57, 0xFF, 9);
    public static readonly uint SOUND_GENERAL_COLLECT_1UP                           = SOUND_ARG_LOAD(3, 0, 0x58, 0xFF, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_BUTTON_PRESS_LOWPRIO   = SOUND_ARG_LOAD(3, 0, 0x5A, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_BUTTON_PRESS       = SOUND_ARG_LOAD(3, 0, 0x5A, 0x40, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_BUTTON_PRESS_2_LOWPRIO = SOUND_ARG_LOAD(3, 1, 0x5A, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_BUTTON_PRESS_2     = SOUND_ARG_LOAD(3, 1, 0x5A, 0x40, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_ELEVATOR_MOVE      = SOUND_ARG_LOAD(3, 0, 0x5B, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_ELEVATOR_MOVE_2    = SOUND_ARG_LOAD(3, 1, 0x5B, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_SWISH_AIR          = SOUND_ARG_LOAD(3, 0, 0x5C, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_SWISH_AIR_2        = SOUND_ARG_LOAD(3, 1, 0x5C, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_HAUNTED_CHAIR      = SOUND_ARG_LOAD(3, 0, 0x5D, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_SOFT_LANDING       = SOUND_ARG_LOAD(3, 0, 0x5E, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_HAUNTED_CHAIR_MOVE = SOUND_ARG_LOAD(3, 0, 0x5F, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_BOWSER_PLATFORM    = SOUND_ARG_LOAD(3, 0, 0x62, 0x80, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_BOWSER_PLATFORM_2  = SOUND_ARG_LOAD(3, 1, 0x62, 0x80, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_HEART_SPIN         = SOUND_ARG_LOAD(3, 0, 0x64, 0xC0, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_POUND_WOOD_POST    = SOUND_ARG_LOAD(3, 0, 0x65, 0xC0, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_WATER_LEVEL_TRIG   = SOUND_ARG_LOAD(3, 0, 0x66, 0x80, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_SWITCH_DOOR_OPEN   = SOUND_ARG_LOAD(3, 0, 0x67, 0xA0, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_RED_COIN           = SOUND_ARG_LOAD(3, 0, 0x68, 0x90, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_BIRDS_FLY_AWAY     = SOUND_ARG_LOAD(3, 0, 0x69, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_METAL_POUND        = SOUND_ARG_LOAD(3, 0, 0x6B, 0x80, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_BOING1             = SOUND_ARG_LOAD(3, 0, 0x6C, 0x40, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_BOING2_LOWPRIO     = SOUND_ARG_LOAD(3, 0, 0x6D, 0x20, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_BOING2             = SOUND_ARG_LOAD(3, 0, 0x6D, 0x40, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_YOSHI_WALK         = SOUND_ARG_LOAD(3, 0, 0x6E, 0x20, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_ENEMY_ALERT1       = SOUND_ARG_LOAD(3, 0, 0x6F, 0x30, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_YOSHI_TALK         = SOUND_ARG_LOAD(3, 0, 0x70, 0x30, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_SPLATTERING        = SOUND_ARG_LOAD(3, 0, 0x71, 0x30, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_BOING3             = 0x307;
    /* not verified */ public static readonly uint SOUND_GENERAL_GRAND_STAR         = SOUND_ARG_LOAD(3, 0, 0x73, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_GRAND_STAR_JUMP    = SOUND_ARG_LOAD(3, 0, 0x74, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_BOAT_ROCK          = SOUND_ARG_LOAD(3, 0, 0x75, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_GENERAL_VANISH_SFX         = SOUND_ARG_LOAD(3, 0, 0x76, 0x20, 8);

    /* Environment Sound Effects */
    /* not verified */ public static readonly uint SOUND_ENV_WATERFALL1             = SOUND_ARG_LOAD(4, 0, 0x00, 0x00, 0);
    /* not verified */ public static readonly uint SOUND_ENV_WATERFALL2             = SOUND_ARG_LOAD(4, 0, 0x01, 0x00, 0);
    /* not verified */ public static readonly uint SOUND_ENV_ELEVATOR1              = SOUND_ARG_LOAD(4, 0, 0x02, 0x00, 0);
    /* not verified */ public static readonly uint SOUND_ENV_DRONING1               = SOUND_ARG_LOAD(4, 1, 0x03, 0x00, 0);
    /* not verified */ public static readonly uint SOUND_ENV_DRONING2               = SOUND_ARG_LOAD(4, 0, 0x04, 0x00, 0);
    /* not verified */ public static readonly uint SOUND_ENV_WIND1                  = SOUND_ARG_LOAD(4, 0, 0x05, 0x00, 0);
    /* not verified */ public static readonly uint SOUND_ENV_MOVING_SAND_SNOW       = 0x400;
    /* not verified */ public static readonly uint SOUND_ENV_UNK07                  = 0x400;
    /* not verified */ public static readonly uint SOUND_ENV_ELEVATOR2              = SOUND_ARG_LOAD(4, 0, 0x08, 0x00, 0);
    /* not verified */ public static readonly uint SOUND_ENV_WATER                  = SOUND_ARG_LOAD(4, 0, 0x09, 0x00, 0);
    /* not verified */ public static readonly uint SOUND_ENV_UNKNOWN2               = SOUND_ARG_LOAD(4, 0, 0x0A, 0x00, 0);
    /* not verified */ public static readonly uint SOUND_ENV_BOAT_ROCKING1          = SOUND_ARG_LOAD(4, 0, 0x0B, 0x00, 0);
    /* not verified */ public static readonly uint SOUND_ENV_ELEVATOR3              = SOUND_ARG_LOAD(4, 0, 0x0C, 0x00, 0);
    /* not verified */ public static readonly uint SOUND_ENV_ELEVATOR4              = SOUND_ARG_LOAD(4, 0, 0x0D, 0x00, 0);
    /* not verified */ public static readonly uint SOUND_ENV_ELEVATOR4_2            = SOUND_ARG_LOAD(4, 1, 0x0D, 0x00, 0);
    /* not verified */ public static readonly uint SOUND_ENV_MOVINGSAND             = SOUND_ARG_LOAD(4, 0, 0x0E, 0x00, 0);
    /* not verified */ public static readonly uint SOUND_ENV_MERRY_GO_ROUND_CREAKING    = SOUND_ARG_LOAD(4, 0, 0x0F, 0x40, 0);
    /* not verified */ public static readonly uint SOUND_ENV_WIND2                  = SOUND_ARG_LOAD(4, 0, 0x10, 0x80, 0);
    /* not verified */ public static readonly uint SOUND_ENV_UNK12                  = 0x401;
    /* not verified */ public static readonly uint SOUND_ENV_SLIDING                = SOUND_ARG_LOAD(4, 0, 0x13, 0x00, 0);
    /* not verified */ public static readonly uint SOUND_ENV_STAR                   = SOUND_ARG_LOAD(4, 0, 0x14, 0x00, 1);
    /* not verified */ public static readonly uint SOUND_ENV_UNKNOWN4               = SOUND_ARG_LOAD(4, 1, 0x15, 0x00, 0);
    /* not verified */ public static readonly uint SOUND_ENV_WATER_DRAIN            = SOUND_ARG_LOAD(4, 1, 0x16, 0x00, 0);
    /* not verified */ public static readonly uint SOUND_ENV_METAL_BOX_PUSH         = SOUND_ARG_LOAD(4, 0, 0x17, 0x80, 0);
    /* not verified */ public static readonly uint SOUND_ENV_SINK_QUICKSAND         = SOUND_ARG_LOAD(4, 0, 0x18, 0x80, 0);

    /* Object Sound Effects */
    public static readonly uint SOUND_OBJ_SUSHI_SHARK_WATER_SOUND                   = SOUND_ARG_LOAD(5, 0, 0x00, 0x80, 8);
    public static readonly uint SOUND_OBJ_MRI_SHOOT                                 = SOUND_ARG_LOAD(5, 0, 0x01, 0x00, 8);
    public static readonly uint SOUND_OBJ_BABY_PENGUIN_WALK                         = SOUND_ARG_LOAD(5, 0, 0x02, 0x00, 8);
    public static readonly uint SOUND_OBJ_BOWSER_WALK                               = SOUND_ARG_LOAD(5, 0, 0x03, 0x00, 8);
    public static readonly uint SOUND_OBJ_BOWSER_TAIL_PICKUP                        = SOUND_ARG_LOAD(5, 0, 0x05, 0x00, 8);
    public static readonly uint SOUND_OBJ_BOWSER_DEFEATED                           = SOUND_ARG_LOAD(5, 0, 0x06, 0x00, 8);
    public static readonly uint SOUND_OBJ_BOWSER_SPINNING                           = SOUND_ARG_LOAD(5, 0, 0x07, 0x00, 8);
    public static readonly uint SOUND_OBJ_BOWSER_INHALING                           = SOUND_ARG_LOAD(5, 0, 0x08, 0x00, 8);
    public static readonly uint SOUND_OBJ_BIG_PENGUIN_WALK                          = SOUND_ARG_LOAD(5, 0, 0x09, 0x80, 8);
    public static readonly uint SOUND_OBJ_BOO_BOUNCE_TOP                            = SOUND_ARG_LOAD(5, 0, 0x0A, 0x00, 8);
    public static readonly uint SOUND_OBJ_BOO_LAUGH_SHORT                           = SOUND_ARG_LOAD(5, 0, 0x0B, 0x00, 8);
    public static readonly uint SOUND_OBJ_THWOMP                                    = SOUND_ARG_LOAD(5, 0, 0x0C, 0xA0, 8);
    /* not verified */ public static readonly uint SOUND_OBJ_CANNON1                = SOUND_ARG_LOAD(5, 0, 0x0D, 0xF0, 8);
    /* not verified */ public static readonly uint SOUND_OBJ_CANNON2                = SOUND_ARG_LOAD(5, 0, 0x0E, 0xF0, 8);
    /* not verified */ public static readonly uint SOUND_OBJ_CANNON3                = SOUND_ARG_LOAD(5, 0, 0x0F, 0xF0, 8);
    /* not verified */ public static readonly uint SOUND_OBJ_JUMP_WALK_WATER        = 0x501;
    /* not verified */ public static readonly uint SOUND_OBJ_UNKNOWN2               = SOUND_ARG_LOAD(5, 0, 0x13, 0x00, 8);
    public static readonly uint SOUND_OBJ_MRI_DEATH                                 = SOUND_ARG_LOAD(5, 0, 0x14, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_OBJ_POUNDING1              = SOUND_ARG_LOAD(5, 0, 0x15, 0x50, 8);
    /* not verified */ public static readonly uint SOUND_OBJ_POUNDING1_HIGHPRIO     = SOUND_ARG_LOAD(5, 0, 0x15, 0x80, 8);
    public static readonly uint SOUND_OBJ_WHOMP_LOWPRIO                             = SOUND_ARG_LOAD(5, 0, 0x16, 0x60, 8);
    public static readonly uint SOUND_OBJ_KING_BOBOMB                               = SOUND_ARG_LOAD(5, 0, 0x16, 0x80, 8);
    /* not verified */ public static readonly uint SOUND_OBJ_BULLY_METAL            = SOUND_ARG_LOAD(5, 0, 0x17, 0x80, 8);
    /* not verified */ public static readonly uint SOUND_OBJ_BULLY_EXPLODE          = SOUND_ARG_LOAD(5, 0, 0x18, 0xA0, 8);
    /* not verified */ public static readonly uint SOUND_OBJ_BULLY_EXPLODE_2        = SOUND_ARG_LOAD(5, 1, 0x18, 0xA0, 8);
    /* not verified */ public static readonly uint SOUND_OBJ_POUNDING_CANNON        = SOUND_ARG_LOAD(5, 0, 0x1A, 0x50, 8);
    /* not verified */ public static readonly uint SOUND_OBJ_BULLY_WALK             = SOUND_ARG_LOAD(5, 0, 0x1B, 0x30, 8);
    /* not verified */ public static readonly uint SOUND_OBJ_UNKNOWN3               = SOUND_ARG_LOAD(5, 0, 0x1D, 0x80, 8);
    /* not verified */ public static readonly uint SOUND_OBJ_UNKNOWN4               = SOUND_ARG_LOAD(5, 0, 0x1E, 0xA0, 8);
    public static readonly uint SOUND_OBJ_BABY_PENGUIN_DIVE                         = SOUND_ARG_LOAD(5, 0, 0x1F, 0x40, 8);
    public static readonly uint SOUND_OBJ_GOOMBA_WALK                               = SOUND_ARG_LOAD(5, 0, 0x20, 0x00, 8);
    public static readonly uint SOUND_OBJ_UKIKI_CHATTER_LONG                        = SOUND_ARG_LOAD(5, 0, 0x21, 0x00, 8);
    public static readonly uint SOUND_OBJ_MONTY_MOLE_ATTACK                         = SOUND_ARG_LOAD(5, 0, 0x22, 0x00, 8);
    public static readonly uint SOUND_OBJ_EVIL_LAKITU_THROW                         = SOUND_ARG_LOAD(5, 0, 0x22, 0x20, 8);
    /* not verified */ public static readonly uint SOUND_OBJ_UNK23                  = 0x502;
    public static readonly uint SOUND_OBJ_DYING_ENEMY1                              = SOUND_ARG_LOAD(5, 0, 0x24, 0x40, 8);
    /* not verified */ public static readonly uint SOUND_OBJ_CANNON4                = SOUND_ARG_LOAD(5, 0, 0x25, 0x40, 8);
    /* not verified */ public static readonly uint SOUND_OBJ_DYING_ENEMY2           = 0x502;
    public static readonly uint SOUND_OBJ_BOBOMB_WALK                               = SOUND_ARG_LOAD(5, 0, 0x27, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_OBJ_SOMETHING_LANDING      = SOUND_ARG_LOAD(5, 0, 0x28, 0x80, 8);
    /* not verified */ public static readonly uint SOUND_OBJ_DIVING_IN_WATER        = SOUND_ARG_LOAD(5, 0, 0x29, 0xA0, 8);
    /* not verified */ public static readonly uint SOUND_OBJ_SNOW_SAND1             = SOUND_ARG_LOAD(5, 0, 0x2A, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_OBJ_SNOW_SAND2             = SOUND_ARG_LOAD(5, 0, 0x2B, 0x00, 8);
    public static readonly uint SOUND_OBJ_DEFAULT_DEATH                             = SOUND_ARG_LOAD(5, 0, 0x2C, 0x80, 8);
    public static readonly uint SOUND_OBJ_BIG_PENGUIN_YELL                          = SOUND_ARG_LOAD(5, 0, 0x2D, 0x00, 8);
    public static readonly uint SOUND_OBJ_WATER_BOMB_BOUNCING                       = SOUND_ARG_LOAD(5, 0, 0x2E, 0x80, 8);
    public static readonly uint SOUND_OBJ_GOOMBA_ALERT                              = SOUND_ARG_LOAD(5, 0, 0x2F, 0x00, 8);
    public static readonly uint SOUND_OBJ_WIGGLER_JUMP                              = SOUND_ARG_LOAD(5, 0, 0x2F, 0x60, 8);
    /* not verified */ public static readonly uint SOUND_OBJ_STOMPED                = SOUND_ARG_LOAD(5, 0, 0x30, 0x80, 8);
    /* not verified */ public static readonly uint SOUND_OBJ_UNKNOWN6               = SOUND_ARG_LOAD(5, 0, 0x31, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_OBJ_DIVING_INTO_WATER      = SOUND_ARG_LOAD(5, 0, 0x32, 0x40, 8);
    public static readonly uint SOUND_OBJ_PIRANHA_PLANT_SHRINK                      = SOUND_ARG_LOAD(5, 0, 0x33, 0x40, 8);
    public static readonly uint SOUND_OBJ_KOOPA_THE_QUICK_WALK                      = SOUND_ARG_LOAD(5, 0, 0x34, 0x20, 8);
    public static readonly uint SOUND_OBJ_KOOPA_WALK                                = SOUND_ARG_LOAD(5, 0, 0x35, 0x00, 8);
    public static readonly uint SOUND_OBJ_BULLY_WALKING                             = SOUND_ARG_LOAD(5, 0, 0x36, 0x60, 8);
    public static readonly uint SOUND_OBJ_DORRIE                                    = SOUND_ARG_LOAD(5, 0, 0x37, 0x60, 8);
    public static readonly uint SOUND_OBJ_BOWSER_LAUGH                              = SOUND_ARG_LOAD(5, 0, 0x38, 0x80, 8);
    public static readonly uint SOUND_OBJ_UKIKI_CHATTER_SHORT                       = SOUND_ARG_LOAD(5, 0, 0x39, 0x00, 8);
    public static readonly uint SOUND_OBJ_UKIKI_CHATTER_IDLE                        = SOUND_ARG_LOAD(5, 0, 0x3A, 0x00, 8);
    public static readonly uint SOUND_OBJ_UKIKI_STEP_DEFAULT                        = SOUND_ARG_LOAD(5, 0, 0x3B, 0x00, 8);
    public static readonly uint SOUND_OBJ_UKIKI_STEP_LEAVES                         = SOUND_ARG_LOAD(5, 0, 0x3C, 0x00, 8);
    public static readonly uint SOUND_OBJ_KOOPA_TALK                                = SOUND_ARG_LOAD(5, 0, 0x3D, 0xA0, 8);
    public static readonly uint SOUND_OBJ_KOOPA_DAMAGE                              = SOUND_ARG_LOAD(5, 0, 0x3E, 0xA0, 8);
    /* not verified */ public static readonly uint SOUND_OBJ_KLEPTO1                = SOUND_ARG_LOAD(5, 0, 0x3F, 0x40, 8);
    /* not verified */ public static readonly uint SOUND_OBJ_KLEPTO2                = SOUND_ARG_LOAD(5, 0, 0x40, 0x60, 8);
    public static readonly uint SOUND_OBJ_KING_BOBOMB_TALK                          = SOUND_ARG_LOAD(5, 0, 0x41, 0x00, 8);
    public static readonly uint SOUND_OBJ_KING_BOBOMB_JUMP                          = SOUND_ARG_LOAD(5, 0, 0x46, 0x80, 8);
    public static readonly uint SOUND_OBJ_KING_WHOMP_DEATH                          = SOUND_ARG_LOAD(5, 1, 0x47, 0xC0, 8);
    public static readonly uint SOUND_OBJ_BOO_LAUGH_LONG                            = SOUND_ARG_LOAD(5, 0, 0x48, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_OBJ_EEL                    = SOUND_ARG_LOAD(5, 0, 0x4A, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_OBJ_EEL_2                  = SOUND_ARG_LOAD(5, 2, 0x4A, 0x00, 8);
    public static readonly uint SOUND_OBJ_EYEROK_SHOW_EYE                           = SOUND_ARG_LOAD(5, 2, 0x4B, 0x00, 8);
    public static readonly uint SOUND_OBJ_MR_BLIZZARD_ALERT                         = SOUND_ARG_LOAD(5, 0, 0x4C, 0x00, 8);
    public static readonly uint SOUND_OBJ_SNUFIT_SHOOT                              = SOUND_ARG_LOAD(5, 0, 0x4D, 0x00, 8);
    public static readonly uint SOUND_OBJ_SKEETER_WALK                              = SOUND_ARG_LOAD(5, 0, 0x4E, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_OBJ_WALKING_WATER          = SOUND_ARG_LOAD(5, 0, 0x4F, 0x00, 8);
    public static readonly uint SOUND_OBJ_BIRD_CHIRP3                               = SOUND_ARG_LOAD(5, 0, 0x51, 0x40, 0);
    public static readonly uint SOUND_OBJ_PIRANHA_PLANT_APPEAR                      = SOUND_ARG_LOAD(5, 0, 0x54, 0x20, 8);
    public static readonly uint SOUND_OBJ_FLAME_BLOWN                               = SOUND_ARG_LOAD(5, 0, 0x55, 0x80, 8);
    public static readonly uint SOUND_OBJ_MAD_PIANO_CHOMPING                        = SOUND_ARG_LOAD(5, 2, 0x56, 0x40, 8);
    public static readonly uint SOUND_OBJ_BOBOMB_BUDDY_TALK                         = SOUND_ARG_LOAD(5, 0, 0x58, 0x40, 8);
    /* not verified */ public static readonly uint SOUND_OBJ_SPINY_UNK59            = SOUND_ARG_LOAD(5, 0, 0x59, 0x10, 8);
    public static readonly uint SOUND_OBJ_WIGGLER_HIGH_PITCH                        = SOUND_ARG_LOAD(5, 0, 0x5C, 0x40, 8);
    public static readonly uint SOUND_OBJ_HEAVEHO_TOSSED                            = SOUND_ARG_LOAD(5, 0, 0x5D, 0x40, 8);
    /* not verified */ public static readonly uint SOUND_OBJ_WIGGLER_DEATH          = 0x505;
    public static readonly uint SOUND_OBJ_BOWSER_INTRO_LAUGH                        = SOUND_ARG_LOAD(5, 0, 0x5F, 0x80, 9);
    /* not verified */ public static readonly uint SOUND_OBJ_ENEMY_DEATH_HIGH       = SOUND_ARG_LOAD(5, 0, 0x60, 0xB0, 8);
    /* not verified */ public static readonly uint SOUND_OBJ_ENEMY_DEATH_LOW        = SOUND_ARG_LOAD(5, 0, 0x61, 0xB0, 8);
    public static readonly uint SOUND_OBJ_SWOOP_DEATH                               = SOUND_ARG_LOAD(5, 0, 0x62, 0xB0, 8);
    public static readonly uint SOUND_OBJ_KOOPA_FLYGUY_DEATH                        = SOUND_ARG_LOAD(5, 0, 0x63, 0xB0, 8);
    public static readonly uint SOUND_OBJ_POKEY_DEATH                               = SOUND_ARG_LOAD(5, 0, 0x63, 0xC0, 8);
    /* not verified */ public static readonly uint SOUND_OBJ_SNOWMAN_BOUNCE         = SOUND_ARG_LOAD(5, 0, 0x64, 0xC0, 8);
    public static readonly uint SOUND_OBJ_SNOWMAN_EXPLODE                           = SOUND_ARG_LOAD(5, 0, 0x65, 0xD0, 8);
    /* not verified */ public static readonly uint SOUND_OBJ_POUNDING_LOUD          = SOUND_ARG_LOAD(5, 0, 0x68, 0x40, 8);
    /* not verified */ public static readonly uint SOUND_OBJ_MIPS_RABBIT            = SOUND_ARG_LOAD(5, 0, 0x6A, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_OBJ_MIPS_RABBIT_WATER      = SOUND_ARG_LOAD(5, 0, 0x6C, 0x00, 8);
    public static readonly uint SOUND_OBJ_EYEROK_EXPLODE                            = SOUND_ARG_LOAD(5, 0, 0x6D, 0x00, 8);
    public static readonly uint SOUND_OBJ_CHUCKYA_DEATH                             = SOUND_ARG_LOAD(5, 1, 0x6E, 0x00, 8);
    public static readonly uint SOUND_OBJ_WIGGLER_TALK                              = SOUND_ARG_LOAD(5, 0, 0x6F, 0x00, 8);
    public static readonly uint SOUND_OBJ_WIGGLER_ATTACKED                          = SOUND_ARG_LOAD(5, 0, 0x70, 0x60, 8);
    public static readonly uint SOUND_OBJ_WIGGLER_LOW_PITCH                         = SOUND_ARG_LOAD(5, 0, 0x71, 0x20, 8);
    public static readonly uint SOUND_OBJ_SNUFIT_SKEETER_DEATH                      = SOUND_ARG_LOAD(5, 0, 0x72, 0xC0, 8);
    public static readonly uint SOUND_OBJ_BUBBA_CHOMP                               = SOUND_ARG_LOAD(5, 0, 0x73, 0x40, 8);
    public static readonly uint SOUND_OBJ_ENEMY_DEFEAT_SHRINK                       = SOUND_ARG_LOAD(5, 0, 0x74, 0x40, 8);

    public static readonly uint SOUND_AIR_BOWSER_SPIT_FIRE              = SOUND_ARG_LOAD(6, 0, 0x00, 0x00, 0);
    public static readonly uint SOUND_AIR_UNK01                         = 0x6001;
    public static readonly uint SOUND_AIR_LAKITU_FLY                    = SOUND_ARG_LOAD(6, 0, 0x02, 0x80, 0);
    public static readonly uint SOUND_AIR_LAKITU_FLY_HIGHPRIO           = SOUND_ARG_LOAD(6, 0, 0x02, 0xFF, 0);
    public static readonly uint SOUND_AIR_AMP_BUZZ                      = SOUND_ARG_LOAD(6, 0, 0x03, 0x40, 0);
    public static readonly uint SOUND_AIR_BLOW_FIRE                     = SOUND_ARG_LOAD(6, 0, 0x04, 0x80, 0);
    public static readonly uint SOUND_AIR_BLOW_WIND                     = SOUND_ARG_LOAD(6, 0, 0x04, 0x40, 0);
    public static readonly uint SOUND_AIR_ROUGH_SLIDE                   = SOUND_ARG_LOAD(6, 0, 0x05, 0x00, 0);
    public static readonly uint SOUND_AIR_HEAVEHO_MOVE                  = SOUND_ARG_LOAD(6, 0, 0x06, 0x40, 0);
    public static readonly uint SOUND_AIR_UNK07                         = 0x6007;
    public static readonly uint SOUND_AIR_BOBOMB_LIT_FUSE               = SOUND_ARG_LOAD(6, 0, 0x08, 0x60, 0);
    public static readonly uint SOUND_AIR_HOWLING_WIND                  = SOUND_ARG_LOAD(6, 0, 0x09, 0x80, 0);
    public static readonly uint SOUND_AIR_CHUCKYA_MOVE                  = SOUND_ARG_LOAD(6, 0, 0x0A, 0x40, 0);
    public static readonly uint SOUND_AIR_PEACH_TWINKLE                 = SOUND_ARG_LOAD(6, 0, 0x0B, 0x40, 0);
    public static readonly uint SOUND_AIR_CASTLE_OUTDOORS_AMBIENT       = SOUND_ARG_LOAD(6, 0, 0x10, 0x40, 0);

    /* Menu Sound Effects */
    public static readonly uint SOUND_MENU_CHANGE_SELECT                            = SOUND_ARG_LOAD(7, 0, 0x00, 0xF8, 8);
    /* not verified */ public static readonly uint SOUND_MENU_REVERSE_PAUSE         = 0x700;
    public static readonly uint SOUND_MENU_PAUSE                                    = SOUND_ARG_LOAD(7, 0, 0x02, 0xF0, 8);
    public static readonly uint SOUND_MENU_PAUSE_HIGHPRIO                           = SOUND_ARG_LOAD(7, 0, 0x02, 0xFF, 8);
    public static readonly uint SOUND_MENU_PAUSE_2                                  = SOUND_ARG_LOAD(7, 0, 0x03, 0xFF, 8);
    public static readonly uint SOUND_MENU_MESSAGE_APPEAR                           = SOUND_ARG_LOAD(7, 0, 0x04, 0x00, 8);
    public static readonly uint SOUND_MENU_MESSAGE_DISAPPEAR                        = SOUND_ARG_LOAD(7, 0, 0x05, 0x00, 8);
    public static readonly uint SOUND_MENU_CAMERA_ZOOM_IN                           = SOUND_ARG_LOAD(7, 0, 0x06, 0x00, 8);
    public static readonly uint SOUND_MENU_CAMERA_ZOOM_OUT                          = SOUND_ARG_LOAD(7, 0, 0x07, 0x00, 8);
    public static readonly uint SOUND_MENU_PINCH_MARIO_FACE                         = SOUND_ARG_LOAD(7, 0, 0x08, 0x00, 8);
    public static readonly uint SOUND_MENU_LET_GO_MARIO_FACE                        = SOUND_ARG_LOAD(7, 0, 0x09, 0x00, 8);
    public static readonly uint SOUND_MENU_HAND_APPEAR                              = SOUND_ARG_LOAD(7, 0, 0x0A, 0x00, 8);
    public static readonly uint SOUND_MENU_HAND_DISAPPEAR                           = SOUND_ARG_LOAD(7, 0, 0x0B, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_MENU_UNK0C                 = SOUND_ARG_LOAD(7, 0, 0x0C, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_MENU_POWER_METER           = SOUND_ARG_LOAD(7, 0, 0x0D, 0x00, 8);
    public static readonly uint SOUND_MENU_CAMERA_BUZZ                              = SOUND_ARG_LOAD(7, 0, 0x0E, 0x00, 8);
    public static readonly uint SOUND_MENU_CAMERA_TURN                              = SOUND_ARG_LOAD(7, 0, 0x0F, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_MENU_UNK10                 = 0x701;
    public static readonly uint SOUND_MENU_CLICK_FILE_SELECT                        = SOUND_ARG_LOAD(7, 0, 0x11, 0x00, 8);
    /* not verified */ public static readonly uint SOUND_MENU_MESSAGE_NEXT_PAGE     = SOUND_ARG_LOAD(7, 0, 0x13, 0x00, 8);
    public static readonly uint SOUND_MENU_COIN_ITS_A_ME_MARIO                      = SOUND_ARG_LOAD(7, 0, 0x14, 0x00, 8);
    public static readonly uint SOUND_MENU_YOSHI_GAIN_LIVES                         = SOUND_ARG_LOAD(7, 0, 0x15, 0x00, 8);
    public static readonly uint SOUND_MENU_ENTER_PIPE                               = SOUND_ARG_LOAD(7, 0, 0x16, 0xA0, 8);
    public static readonly uint SOUND_MENU_EXIT_PIPE                                = SOUND_ARG_LOAD(7, 0, 0x17, 0xA0, 8);
    public static readonly uint SOUND_MENU_BOWSER_LAUGH                             = SOUND_ARG_LOAD(7, 0, 0x18, 0x80, 8);
    public static readonly uint SOUND_MENU_ENTER_HOLE                               = SOUND_ARG_LOAD(7, 1, 0x19, 0x80, 8);
    /* not verified */ public static readonly uint SOUND_MENU_CLICK_CHANGE_VIEW     = SOUND_ARG_LOAD(7, 0, 0x1A, 0x80, 8);
    /* not verified */ public static readonly uint SOUND_MENU_CAMERA_UNUSED1        = 0x701;
    /* not verified */ public static readonly uint SOUND_MENU_CAMERA_UNUSED2        = 0x701;
    /* not verified */ public static readonly uint SOUND_MENU_MARIO_CASTLE_WARP     = SOUND_ARG_LOAD(7, 0, 0x1D, 0xB0, 8);
    public static readonly uint SOUND_MENU_STAR_SOUND                               = SOUND_ARG_LOAD(7, 0, 0x1E, 0xFF, 8);
    public static readonly uint SOUND_MENU_THANK_YOU_PLAYING_MY_GAME                = SOUND_ARG_LOAD(7, 0, 0x1F, 0xFF, 8);
    /* not verified */ public static readonly uint SOUND_MENU_READ_A_SIGN           = 0x702;
    /* not verified */ public static readonly uint SOUND_MENU_EXIT_A_SIGN           = 0x702;
    /* not verified */ public static readonly uint SOUND_MENU_MARIO_CASTLE_WARP2    = SOUND_ARG_LOAD(7, 0, 0x22, 0x20, 8);
    public static readonly uint SOUND_MENU_STAR_SOUND_OKEY_DOKEY                    = SOUND_ARG_LOAD(7, 0, 0x23, 0xFF, 8);
    public static readonly uint SOUND_MENU_STAR_SOUND_LETS_A_GO                     = SOUND_ARG_LOAD(7, 0, 0x24, 0xFF, 8);

    // US/EU only; an index between 0-7 or 0-4 is added to the sound ID before
    // playing, producing the same sound with different pitch.
    public static readonly uint SOUND_MENU_COLLECT_RED_COIN             = SOUND_ARG_LOAD(7, 8, 0x28, 0x90, 8);
    public static readonly uint SOUND_MENU_COLLECT_SECRET               = SOUND_ARG_LOAD(7, 0, 0x30, 0x20, 8);

    // Channel 8 loads sounds from the same place as channel 3, making it possible
    // to play two channel 3 sounds at once (since just one sound from each channel
    // can play at a given time);.
    public static readonly uint SOUND_GENERAL2_BOBOMB_EXPLOSION         = SOUND_ARG_LOAD(8, 0, 0x2E, 0x20, 8);
    public static readonly uint SOUND_GENERAL2_PURPLE_SWITCH            = SOUND_ARG_LOAD(8, 0, 0x3E, 0xC0, 8);
    public static readonly uint SOUND_GENERAL2_ROTATING_BLOCK_CLICK     = SOUND_ARG_LOAD(8, 0, 0x40, 0x00, 8);
    public static readonly uint SOUND_GENERAL2_SPINDEL_ROLL             = SOUND_ARG_LOAD(8, 0, 0x48, 0x20, 8);
    public static readonly uint SOUND_GENERAL2_PYRAMID_TOP_SPIN         = SOUND_ARG_LOAD(8, 1, 0x4B, 0xE0, 8);
    public static readonly uint SOUND_GENERAL2_PYRAMID_TOP_EXPLOSION    = SOUND_ARG_LOAD(8, 1, 0x4C, 0xF0, 8);
    public static readonly uint SOUND_GENERAL2_BIRD_CHIRP2              = SOUND_ARG_LOAD(8, 0, 0x50, 0x40, 0);
    public static readonly uint SOUND_GENERAL2_SWITCH_TICK_FAST         = SOUND_ARG_LOAD(8, 0, 0x54, 0xF0, 1);
    public static readonly uint SOUND_GENERAL2_SWITCH_TICK_SLOW         = SOUND_ARG_LOAD(8, 0, 0x55, 0xF0, 1);
    public static readonly uint SOUND_GENERAL2_STAR_APPEARS             = SOUND_ARG_LOAD(8, 0, 0x57, 0xFF, 9);
    public static readonly uint SOUND_GENERAL2_ROTATING_BLOCK_ALERT     = SOUND_ARG_LOAD(8, 0, 0x59, 0x00, 8);
    public static readonly uint SOUND_GENERAL2_BOWSER_EXPLODE           = SOUND_ARG_LOAD(8, 0, 0x60, 0x00, 8);
    public static readonly uint SOUND_GENERAL2_BOWSER_KEY               = SOUND_ARG_LOAD(8, 0, 0x61, 0x00, 8);
    public static readonly uint SOUND_GENERAL2_1UP_APPEAR               = SOUND_ARG_LOAD(8, 0, 0x63, 0xD0, 8);
    public static readonly uint SOUND_GENERAL2_RIGHT_ANSWER             = SOUND_ARG_LOAD(8, 0, 0x6A, 0xA0, 8);

    // Channel 9 loads sounds from the same place as channel 5.
    public static readonly uint SOUND_OBJ2_BOWSER_ROAR                  = SOUND_ARG_LOAD(9, 0, 0x04, 0x00, 8);
    public static readonly uint SOUND_OBJ2_PIRANHA_PLANT_BITE           = SOUND_ARG_LOAD(9, 0, 0x10, 0x50, 8);
    public static readonly uint SOUND_OBJ2_PIRANHA_PLANT_DYING          = SOUND_ARG_LOAD(9, 0, 0x11, 0x60, 8);
    public static readonly uint SOUND_OBJ2_BOWSER_PUZZLE_PIECE_MOVE     = SOUND_ARG_LOAD(9, 0, 0x19, 0x20, 8);
    public static readonly uint SOUND_OBJ2_BULLY_ATTACKED               = SOUND_ARG_LOAD(9, 0, 0x1C, 0x00, 8);
    public static readonly uint SOUND_OBJ2_KING_BOBOMB_DAMAGE           = SOUND_ARG_LOAD(9, 1, 0x42, 0x40, 8);
    public static readonly uint SOUND_OBJ2_SCUTTLEBUG_WALK              = SOUND_ARG_LOAD(9, 0, 0x43, 0x40, 8);
    public static readonly uint SOUND_OBJ2_SCUTTLEBUG_ALERT             = SOUND_ARG_LOAD(9, 0, 0x44, 0x40, 8);
    public static readonly uint SOUND_OBJ2_BABY_PENGUIN_YELL            = SOUND_ARG_LOAD(9, 0, 0x45, 0x00, 8);
    public static readonly uint SOUND_OBJ2_SWOOP                        = SOUND_ARG_LOAD(9, 0, 0x49, 0x00, 8);
    public static readonly uint SOUND_OBJ2_BIRD_CHIRP1                  = SOUND_ARG_LOAD(9, 0, 0x52, 0x40, 0);
    public static readonly uint SOUND_OBJ2_LARGE_BULLY_ATTACKED         = SOUND_ARG_LOAD(9, 0, 0x57, 0x00, 8);
    public static readonly uint SOUND_OBJ2_EYEROK_SOUND_SHORT           = SOUND_ARG_LOAD(9, 3, 0x5A, 0x00, 8);
    public static readonly uint SOUND_OBJ2_WHOMP_SOUND_SHORT            = SOUND_ARG_LOAD(9, 3, 0x5A, 0xC0, 8);
    public static readonly uint SOUND_OBJ2_EYEROK_SOUND_LONG            = SOUND_ARG_LOAD(9, 2, 0x5B, 0x00, 8);
    public static readonly uint SOUND_OBJ2_BOWSER_TELEPORT              = SOUND_ARG_LOAD(9, 0, 0x66, 0x80, 8);
    public static readonly uint SOUND_OBJ2_MONTY_MOLE_APPEAR            = SOUND_ARG_LOAD(9, 0, 0x67, 0x80, 8);
    public static readonly uint SOUND_OBJ2_BOSS_DIALOG_GRUNT            = SOUND_ARG_LOAD(9, 0, 0x69, 0x40, 8);
    public static readonly uint SOUND_OBJ2_MRI_SPINNING                 = SOUND_ARG_LOAD(9, 0, 0x6B, 0x00, 8);


    // mario_animation_ids.h
    public enum MarioAnimID : int
    {
        /* 0x00 */ MARIO_ANIM_SLOW_LEDGE_GRAB,
        /* 0x01 */ MARIO_ANIM_FALL_OVER_BACKWARDS,
        /* 0x02 */ MARIO_ANIM_BACKWARD_AIR_KB,
        /* 0x03 */ MARIO_ANIM_DYING_ON_BACK,
        /* 0x04 */ MARIO_ANIM_BACKFLIP,
        /* 0x05 */ MARIO_ANIM_CLIMB_UP_POLE,
        /* 0x06 */ MARIO_ANIM_GRAB_POLE_SHORT,
        /* 0x07 */ MARIO_ANIM_GRAB_POLE_SWING_PART1,
        /* 0x08 */ MARIO_ANIM_GRAB_POLE_SWING_PART2,
        /* 0x09 */ MARIO_ANIM_HANDSTAND_IDLE,
        /* 0x0A */ MARIO_ANIM_HANDSTAND_JUMP,
        /* 0x0B */ MARIO_ANIM_START_HANDSTAND,
        /* 0x0C */ MARIO_ANIM_RETURN_FROM_HANDSTAND,
        /* 0x0D */ MARIO_ANIM_IDLE_ON_POLE,
        /* 0x0E */ MARIO_ANIM_A_POSE,
        /* 0x0F */ MARIO_ANIM_SKID_ON_GROUND,
        /* 0x10 */ MARIO_ANIM_STOP_SKID,
        /* 0x11 */ MARIO_ANIM_CROUCH_FROM_FAST_LONGJUMP,
        /* 0x12 */ MARIO_ANIM_CROUCH_FROM_SLOW_LONGJUMP,
        /* 0x13 */ MARIO_ANIM_FAST_LONGJUMP,
        /* 0x14 */ MARIO_ANIM_SLOW_LONGJUMP,
        /* 0x15 */ MARIO_ANIM_AIRBORNE_ON_STOMACH,
        /* 0x16 */ MARIO_ANIM_WALK_WITH_LIGHT_OBJ,
        /* 0x17 */ MARIO_ANIM_RUN_WITH_LIGHT_OBJ,
        /* 0x18 */ MARIO_ANIM_SLOW_WALK_WITH_LIGHT_OBJ,
        /* 0x19 */ MARIO_ANIM_SHIVERING_WARMING_HAND,
        /* 0x1A */ MARIO_ANIM_SHIVERING_RETURN_TO_IDLE,
        /* 0x1B */ MARIO_ANIM_SHIVERING,
        /* 0x1C */ MARIO_ANIM_CLIMB_DOWN_LEDGE,
        /* 0x1D */ MARIO_ANIM_CREDITS_WAVING,
        /* 0x1E */ MARIO_ANIM_CREDITS_LOOK_UP,
        /* 0x1F */ MARIO_ANIM_CREDITS_RETURN_FROM_LOOK_UP,
        /* 0x20 */ MARIO_ANIM_CREDITS_RAISE_HAND,
        /* 0x21 */ MARIO_ANIM_CREDITS_LOWER_HAND,
        /* 0x22 */ MARIO_ANIM_CREDITS_TAKE_OFF_CAP,
        /* 0x23 */ MARIO_ANIM_CREDITS_START_WALK_LOOK_UP,
        /* 0x24 */ MARIO_ANIM_CREDITS_LOOK_BACK_THEN_RUN,
        /* 0x25 */ MARIO_ANIM_FINAL_BOWSER_RAISE_HAND_SPIN,
        /* 0x26 */ MARIO_ANIM_FINAL_BOWSER_WING_CAP_TAKE_OFF,
        /* 0x27 */ MARIO_ANIM_CREDITS_PEACE_SIGN,
        /* 0x28 */ MARIO_ANIM_STAND_UP_FROM_LAVA_BOOST,
        /* 0x29 */ MARIO_ANIM_FIRE_LAVA_BURN,
        /* 0x2A */ MARIO_ANIM_WING_CAP_FLY,
        /* 0x2B */ MARIO_ANIM_HANG_ON_OWL,
        /* 0x2C */ MARIO_ANIM_LAND_ON_STOMACH,
        /* 0x2D */ MARIO_ANIM_AIR_FORWARD_KB,
        /* 0x2E */ MARIO_ANIM_DYING_ON_STOMACH,
        /* 0x2F */ MARIO_ANIM_SUFFOCATING,
        /* 0x30 */ MARIO_ANIM_COUGHING,
        /* 0x31 */ MARIO_ANIM_THROW_CATCH_KEY,
        /* 0x32 */ MARIO_ANIM_DYING_FALL_OVER,
        /* 0x33 */ MARIO_ANIM_IDLE_ON_LEDGE,
        /* 0x34 */ MARIO_ANIM_FAST_LEDGE_GRAB,
        /* 0x35 */ MARIO_ANIM_HANG_ON_CEILING,
        /* 0x36 */ MARIO_ANIM_PUT_CAP_ON,
        /* 0x37 */ MARIO_ANIM_TAKE_CAP_OFF_THEN_ON,
        /* 0x38 */ MARIO_ANIM_QUICKLY_PUT_CAP_ON, // unused
        /* 0x39 */ MARIO_ANIM_HEAD_STUCK_IN_GROUND,
        /* 0x3A */ MARIO_ANIM_GROUND_POUND_LANDING,
        /* 0x3B */ MARIO_ANIM_TRIPLE_JUMP_GROUND_POUND,
        /* 0x3C */ MARIO_ANIM_START_GROUND_POUND,
        /* 0x3D */ MARIO_ANIM_GROUND_POUND,
        /* 0x3E */ MARIO_ANIM_BOTTOM_STUCK_IN_GROUND,
        /* 0x3F */ MARIO_ANIM_IDLE_WITH_LIGHT_OBJ,
        /* 0x40 */ MARIO_ANIM_JUMP_LAND_WITH_LIGHT_OBJ,
        /* 0x41 */ MARIO_ANIM_JUMP_WITH_LIGHT_OBJ,
        /* 0x42 */ MARIO_ANIM_FALL_LAND_WITH_LIGHT_OBJ,
        /* 0x43 */ MARIO_ANIM_FALL_WITH_LIGHT_OBJ,
        /* 0x44 */ MARIO_ANIM_FALL_FROM_SLIDING_WITH_LIGHT_OBJ,
        /* 0x45 */ MARIO_ANIM_SLIDING_ON_BOTTOM_WITH_LIGHT_OBJ,
        /* 0x46 */ MARIO_ANIM_STAND_UP_FROM_SLIDING_WITH_LIGHT_OBJ,
        /* 0x47 */ MARIO_ANIM_RIDING_SHELL,
        /* 0x48 */ MARIO_ANIM_WALKING,
        /* 0x49 */ MARIO_ANIM_FORWARD_FLIP, // unused
        /* 0x4A */ MARIO_ANIM_JUMP_RIDING_SHELL,
        /* 0x4B */ MARIO_ANIM_LAND_FROM_DOUBLE_JUMP,
        /* 0x4C */ MARIO_ANIM_DOUBLE_JUMP_FALL,
        /* 0x4D */ MARIO_ANIM_SINGLE_JUMP,
        /* 0x4E */ MARIO_ANIM_LAND_FROM_SINGLE_JUMP,
        /* 0x4F */ MARIO_ANIM_AIR_KICK,
        /* 0x50 */ MARIO_ANIM_DOUBLE_JUMP_RISE,
        /* 0x51 */ MARIO_ANIM_START_FORWARD_SPINNING, // unused
        /* 0x52 */ MARIO_ANIM_THROW_LIGHT_OBJECT,
        /* 0x53 */ MARIO_ANIM_FALL_FROM_SLIDE_KICK,
        /* 0x54 */ MARIO_ANIM_BEND_KNESS_RIDING_SHELL, // unused
        /* 0x55 */ MARIO_ANIM_LEGS_STUCK_IN_GROUND,
        /* 0x56 */ MARIO_ANIM_GENERAL_FALL,
        /* 0x57 */ MARIO_ANIM_GENERAL_LAND,
        /* 0x58 */ MARIO_ANIM_BEING_GRABBED,
        /* 0x59 */ MARIO_ANIM_GRAB_HEAVY_OBJECT,
        /* 0x5A */ MARIO_ANIM_SLOW_LAND_FROM_DIVE,
        /* 0x5B */ MARIO_ANIM_FLY_FROM_CANNON,
        /* 0x5C */ MARIO_ANIM_MOVE_ON_WIRE_NET_RIGHT,
        /* 0x5D */ MARIO_ANIM_MOVE_ON_WIRE_NET_LEFT,
        /* 0x5E */ MARIO_ANIM_MISSING_CAP,
        /* 0x5F */ MARIO_ANIM_PULL_DOOR_WALK_IN,
        /* 0x60 */ MARIO_ANIM_PUSH_DOOR_WALK_IN,
        /* 0x61 */ MARIO_ANIM_UNLOCK_DOOR,
        /* 0x62 */ MARIO_ANIM_START_REACH_POCKET, // unused, reaching keys maybe?
        /* 0x63 */ MARIO_ANIM_REACH_POCKET, // unused
        /* 0x64 */ MARIO_ANIM_STOP_REACH_POCKET, // unused
        /* 0x65 */ MARIO_ANIM_GROUND_THROW,
        /* 0x66 */ MARIO_ANIM_GROUND_KICK,
        /* 0x67 */ MARIO_ANIM_FIRST_PUNCH,
        /* 0x68 */ MARIO_ANIM_SECOND_PUNCH,
        /* 0x69 */ MARIO_ANIM_FIRST_PUNCH_FAST,
        /* 0x6A */ MARIO_ANIM_SECOND_PUNCH_FAST,
        /* 0x6B */ MARIO_ANIM_PICK_UP_LIGHT_OBJ,
        /* 0x6C */ MARIO_ANIM_PUSHING,
        /* 0x6D */ MARIO_ANIM_START_RIDING_SHELL,
        /* 0x6E */ MARIO_ANIM_PLACE_LIGHT_OBJ,
        /* 0x6F */ MARIO_ANIM_FORWARD_SPINNING,
        /* 0x70 */ MARIO_ANIM_BACKWARD_SPINNING,
        /* 0x71 */ MARIO_ANIM_BREAKDANCE,
        /* 0x72 */ MARIO_ANIM_RUNNING,
        /* 0x73 */ MARIO_ANIM_RUNNING_UNUSED, // unused duplicate, originally part 2?
        /* 0x74 */ MARIO_ANIM_SOFT_BACK_KB,
        /* 0x75 */ MARIO_ANIM_SOFT_FRONT_KB,
        /* 0x76 */ MARIO_ANIM_DYING_IN_QUICKSAND,
        /* 0x77 */ MARIO_ANIM_IDLE_IN_QUICKSAND,
        /* 0x78 */ MARIO_ANIM_MOVE_IN_QUICKSAND,
        /* 0x79 */ MARIO_ANIM_ELECTROCUTION,
        /* 0x7A */ MARIO_ANIM_SHOCKED,
        /* 0x7B */ MARIO_ANIM_BACKWARD_KB,
        /* 0x7C */ MARIO_ANIM_FORWARD_KB,
        /* 0x7D */ MARIO_ANIM_IDLE_HEAVY_OBJ,
        /* 0x7E */ MARIO_ANIM_STAND_AGAINST_WALL,
        /* 0x7F */ MARIO_ANIM_SIDESTEP_LEFT,
        /* 0x80 */ MARIO_ANIM_SIDESTEP_RIGHT,
        /* 0x81 */ MARIO_ANIM_START_SLEEP_IDLE,
        /* 0x82 */ MARIO_ANIM_START_SLEEP_SCRATCH,
        /* 0x83 */ MARIO_ANIM_START_SLEEP_YAWN,
        /* 0x84 */ MARIO_ANIM_START_SLEEP_SITTING,
        /* 0x85 */ MARIO_ANIM_SLEEP_IDLE,
        /* 0x86 */ MARIO_ANIM_SLEEP_START_LYING,
        /* 0x87 */ MARIO_ANIM_SLEEP_LYING,
        /* 0x88 */ MARIO_ANIM_DIVE,
        /* 0x89 */ MARIO_ANIM_SLIDE_DIVE,
        /* 0x8A */ MARIO_ANIM_GROUND_BONK,
        /* 0x8B */ MARIO_ANIM_STOP_SLIDE_LIGHT_OBJ,
        /* 0x8C */ MARIO_ANIM_SLIDE_KICK,
        /* 0x8D */ MARIO_ANIM_CROUCH_FROM_SLIDE_KICK,
        /* 0x8E */ MARIO_ANIM_SLIDE_MOTIONLESS, // unused
        /* 0x8F */ MARIO_ANIM_STOP_SLIDE,
        /* 0x90 */ MARIO_ANIM_FALL_FROM_SLIDE,
        /* 0x91 */ MARIO_ANIM_SLIDE,
        /* 0x92 */ MARIO_ANIM_TIPTOE,
        /* 0x93 */ MARIO_ANIM_TWIRL_LAND,
        /* 0x94 */ MARIO_ANIM_TWIRL,
        /* 0x95 */ MARIO_ANIM_START_TWIRL,
        /* 0x96 */ MARIO_ANIM_STOP_CROUCHING,
        /* 0x97 */ MARIO_ANIM_START_CROUCHING,
        /* 0x98 */ MARIO_ANIM_CROUCHING,
        /* 0x99 */ MARIO_ANIM_CRAWLING,
        /* 0x9A */ MARIO_ANIM_STOP_CRAWLING,
        /* 0x9B */ MARIO_ANIM_START_CRAWLING,
        /* 0x9C */ MARIO_ANIM_SUMMON_STAR,
        /* 0x9D */ MARIO_ANIM_RETURN_STAR_APPROACH_DOOR,
        /* 0x9E */ MARIO_ANIM_BACKWARDS_WATER_KB,
        /* 0x9F */ MARIO_ANIM_SWIM_WITH_OBJ_PART1,
        /* 0xA0 */ MARIO_ANIM_SWIM_WITH_OBJ_PART2,
        /* 0xA1 */ MARIO_ANIM_FLUTTERKICK_WITH_OBJ,
        /* 0xA2 */ MARIO_ANIM_WATER_ACTION_END_WITH_OBJ, // either swimming or flutterkicking
        /* 0xA3 */ MARIO_ANIM_STOP_GRAB_OBJ_WATER,
        /* 0xA4 */ MARIO_ANIM_WATER_IDLE_WITH_OBJ,
        /* 0xA5 */ MARIO_ANIM_DROWNING_PART1,
        /* 0xA6 */ MARIO_ANIM_DROWNING_PART2,
        /* 0xA7 */ MARIO_ANIM_WATER_DYING,
        /* 0xA8 */ MARIO_ANIM_WATER_FORWARD_KB,
        /* 0xA9 */ MARIO_ANIM_FALL_FROM_WATER,
        /* 0xAA */ MARIO_ANIM_SWIM_PART1,
        /* 0xAB */ MARIO_ANIM_SWIM_PART2,
        /* 0xAC */ MARIO_ANIM_FLUTTERKICK,
        /* 0xAD */ MARIO_ANIM_WATER_ACTION_END, // either swimming or flutterkicking
        /* 0xAE */ MARIO_ANIM_WATER_PICK_UP_OBJ,
        /* 0xAF */ MARIO_ANIM_WATER_GRAB_OBJ_PART2,
        /* 0xB0 */ MARIO_ANIM_WATER_GRAB_OBJ_PART1,
        /* 0xB1 */ MARIO_ANIM_WATER_THROW_OBJ,
        /* 0xB2 */ MARIO_ANIM_WATER_IDLE,
        /* 0xB3 */ MARIO_ANIM_WATER_STAR_DANCE,
        /* 0xB4 */ MARIO_ANIM_RETURN_FROM_WATER_STAR_DANCE,
        /* 0xB5 */ MARIO_ANIM_GRAB_BOWSER,
        /* 0xB6 */ MARIO_ANIM_SWINGING_BOWSER,
        /* 0xB7 */ MARIO_ANIM_RELEASE_BOWSER,
        /* 0xB8 */ MARIO_ANIM_HOLDING_BOWSER,
        /* 0xB9 */ MARIO_ANIM_HEAVY_THROW,
        /* 0xBA */ MARIO_ANIM_WALK_PANTING,
        /* 0xBB */ MARIO_ANIM_WALK_WITH_HEAVY_OBJ,
        /* 0xBC */ MARIO_ANIM_TURNING_PART1,
        /* 0xBD */ MARIO_ANIM_TURNING_PART2,
        /* 0xBE */ MARIO_ANIM_SLIDEFLIP_LAND,
        /* 0XBF */ MARIO_ANIM_SLIDEFLIP,
        /* 0xC0 */ MARIO_ANIM_TRIPLE_JUMP_LAND,
        /* 0xC1 */ MARIO_ANIM_TRIPLE_JUMP,
        /* 0xC2 */ MARIO_ANIM_FIRST_PERSON,
        /* 0xC3 */ MARIO_ANIM_IDLE_HEAD_LEFT,
        /* 0xC4 */ MARIO_ANIM_IDLE_HEAD_RIGHT,
        /* 0xC5 */ MARIO_ANIM_IDLE_HEAD_CENTER,
        /* 0xC6 */ MARIO_ANIM_HANDSTAND_LEFT,
        /* 0xC7 */ MARIO_ANIM_HANDSTAND_RIGHT,
        /* 0xC8 */ MARIO_ANIM_WAKE_FROM_SLEEP,
        /* 0xC9 */ MARIO_ANIM_WAKE_FROM_LYING,
        /* 0xCA */ MARIO_ANIM_START_TIPTOE,
        /* 0xCB */ MARIO_ANIM_SLIDEJUMP, // pole jump and wall kick
        /* 0xCC */ MARIO_ANIM_START_WALLKICK,
        /* 0xCD */ MARIO_ANIM_STAR_DANCE,
        /* 0xCE */ MARIO_ANIM_RETURN_FROM_STAR_DANCE,
        /* 0xCF */ MARIO_ANIM_FORWARD_SPINNING_FLIP,
        /* 0xD0 */ MARIO_ANIM_TRIPLE_JUMP_FLY
    };


    // sm64.h
    public static readonly uint MARIO_NORMAL_CAP =                 0x00000001;
    public static readonly uint MARIO_VANISH_CAP =                 0x00000002;
    public static readonly uint MARIO_METAL_CAP =                  0x00000004;
    public static readonly uint MARIO_WING_CAP =                   0x00000008;
    public static readonly uint MARIO_CAP_ON_HEAD =                0x00000010;
    public static readonly uint MARIO_CAP_IN_HAND =                0x00000020;
    public static readonly uint MARIO_METAL_SHOCK =                0x00000040;
    public static readonly uint MARIO_TELEPORTING =                0x00000080;
    public static readonly uint MARIO_UNKNOWN_08 =                 0x00000100;
    public static readonly uint MARIO_UNKNOWN_13 =                 0x00002000;
    public static readonly uint MARIO_ACTION_SOUND_PLAYED =        0x00010000;
    public static readonly uint MARIO_MARIO_SOUND_PLAYED =         0x00020000;
    public static readonly uint MARIO_UNKNOWN_18 =                 0x00040000;
    public static readonly uint MARIO_PUNCHING =                   0x00100000;
    public static readonly uint MARIO_KICKING =                    0x00200000;
    public static readonly uint MARIO_TRIPPING =                   0x00400000;
    public static readonly uint MARIO_UNKNOWN_25 =                 0x02000000;
    public static readonly uint MARIO_UNKNOWN_30 =                 0x40000000;
    public static readonly uint MARIO_UNKNOWN_31 =                 0x80000000;

    public static readonly uint MARIO_SPECIAL_CAPS =  (MARIO_VANISH_CAP | MARIO_METAL_CAP | MARIO_WING_CAP);
    public static readonly uint MARIO_CAPS =  (MARIO_NORMAL_CAP | MARIO_SPECIAL_CAPS);

    public static readonly uint ACT_GROUP_MASK =        0x000001C0;
    public static readonly uint ACT_GROUP_STATIONARY =  /* 0x00000000 */ (0 << 6);
    public static readonly uint ACT_GROUP_MOVING =      /* 0x00000040 */ (1 << 6);
    public static readonly uint ACT_GROUP_AIRBORNE =    /* 0x00000080 */ (2 << 6);
    public static readonly uint ACT_GROUP_SUBMERGED =   /* 0x000000C0 */ (3 << 6);
    public static readonly uint ACT_GROUP_CUTSCENE =    /* 0x00000100 */ (4 << 6);
    public static readonly uint ACT_GROUP_AUTOMATIC =   /* 0x00000140 */ (5 << 6);
    public static readonly uint ACT_GROUP_OBJECT =      /* 0x00000180 */ (6 << 6);

    public static readonly uint ACT_FLAG_STATIONARY =                   /* 0x00000200 */ (1 <<  9);
    public static readonly uint ACT_FLAG_MOVING =                       /* 0x00000400 */ (1 << 10);
    public static readonly uint ACT_FLAG_AIR =                          /* 0x00000800 */ (1 << 11);
    public static readonly uint ACT_FLAG_INTANGIBLE =                   /* 0x00001000 */ (1 << 12);
    public static readonly uint ACT_FLAG_SWIMMING =                     /* 0x00002000 */ (1 << 13);
    public static readonly uint ACT_FLAG_METAL_WATER =                  /* 0x00004000 */ (1 << 14);
    public static readonly uint ACT_FLAG_SHORT_HITBOX =                 /* 0x00008000 */ (1 << 15);
    public static readonly uint ACT_FLAG_RIDING_SHELL =                 /* 0x00010000 */ (1 << 16);
    public static readonly uint ACT_FLAG_INVULNERABLE =                 /* 0x00020000 */ (1 << 17);
    public static readonly uint ACT_FLAG_BUTT_OR_STOMACH_SLIDE =        /* 0x00040000 */ (1 << 18);
    public static readonly uint ACT_FLAG_DIVING =                       /* 0x00080000 */ (1 << 19);
    public static readonly uint ACT_FLAG_ON_POLE =                      /* 0x00100000 */ (1 << 20);
    public static readonly uint ACT_FLAG_HANGING =                      /* 0x00200000 */ (1 << 21);
    public static readonly uint ACT_FLAG_IDLE =                         /* 0x00400000 */ (1 << 22);
    public static readonly uint ACT_FLAG_ATTACKING =                    /* 0x00800000 */ (1 << 23);
    public static readonly uint ACT_FLAG_ALLOW_VERTICAL_WIND_ACTION =   /* 0x01000000 */ (1 << 24);
    public static readonly uint ACT_FLAG_CONTROL_JUMP_HEIGHT =          /* 0x02000000 */ (1 << 25);
    public static readonly uint ACT_FLAG_ALLOW_FIRST_PERSON =           /* 0x04000000 */ (1 << 26);
    public static readonly uint ACT_FLAG_PAUSE_EXIT =                   /* 0x08000000 */ (1 << 27);
    public static readonly uint ACT_FLAG_SWIMMING_OR_FLYING =           /* 0x10000000 */ (1 << 28);
    public static readonly uint ACT_FLAG_WATER_OR_TEXT =                /* 0x20000000 */ (1 << 29);
    public static readonly int ACT_FLAG_THROWING =                     /* 0x80000000 */ (1 << 31);

    public enum Action: uint
    {
        ACT_UNINITIALIZED =               0x00000000, // (0x000)

        // group 0x000: stationary actions
        ACT_IDLE =                        0x0C400201, // (0x001 | ACT_FLAG_STATIONARY | ACT_FLAG_IDLE | ACT_FLAG_ALLOW_FIRST_PERSON | ACT_FLAG_PAUSE_EXIT)
        ACT_START_SLEEPING =              0x0C400202, // (0x002 | ACT_FLAG_STATIONARY | ACT_FLAG_IDLE | ACT_FLAG_ALLOW_FIRST_PERSON | ACT_FLAG_PAUSE_EXIT)
        ACT_SLEEPING =                    0x0C000203, // (0x003 | ACT_FLAG_STATIONARY | ACT_FLAG_ALLOW_FIRST_PERSON | ACT_FLAG_PAUSE_EXIT)
        ACT_WAKING_UP =                   0x0C000204, // (0x004 | ACT_FLAG_STATIONARY | ACT_FLAG_ALLOW_FIRST_PERSON | ACT_FLAG_PAUSE_EXIT)
        ACT_PANTING =                     0x0C400205, // (0x005 | ACT_FLAG_STATIONARY | ACT_FLAG_IDLE | ACT_FLAG_ALLOW_FIRST_PERSON | ACT_FLAG_PAUSE_EXIT)
        ACT_HOLD_PANTING_UNUSED =         0x08000206, // (0x006 | ACT_FLAG_STATIONARY | ACT_FLAG_PAUSE_EXIT)
        ACT_HOLD_IDLE =                   0x08000207, // (0x007 | ACT_FLAG_STATIONARY | ACT_FLAG_PAUSE_EXIT)
        ACT_HOLD_HEAVY_IDLE =             0x08000208, // (0x008 | ACT_FLAG_STATIONARY | ACT_FLAG_PAUSE_EXIT)
        ACT_STANDING_AGAINST_WALL =       0x0C400209, // (0x009 | ACT_FLAG_STATIONARY | ACT_FLAG_IDLE | ACT_FLAG_ALLOW_FIRST_PERSON | ACT_FLAG_PAUSE_EXIT)
        ACT_COUGHING =                    0x0C40020A, // (0x00A | ACT_FLAG_STATIONARY | ACT_FLAG_IDLE | ACT_FLAG_ALLOW_FIRST_PERSON | ACT_FLAG_PAUSE_EXIT)
        ACT_SHIVERING =                   0x0C40020B, // (0x00B | ACT_FLAG_STATIONARY | ACT_FLAG_IDLE | ACT_FLAG_ALLOW_FIRST_PERSON | ACT_FLAG_PAUSE_EXIT)
        ACT_IN_QUICKSAND =                0x0002020D, // (0x00D | ACT_FLAG_STATIONARY | ACT_FLAG_INVULNERABLE)
        ACT_UNKNOWN_0002020E =            0x0002020E, // (0x00E | ACT_FLAG_STATIONARY | ACT_FLAG_INVULNERABLE)
        ACT_CROUCHING =                   0x0C008220, // (0x020 | ACT_FLAG_STATIONARY | ACT_FLAG_SHORT_HITBOX | ACT_FLAG_ALLOW_FIRST_PERSON | ACT_FLAG_PAUSE_EXIT)
        ACT_START_CROUCHING =             0x0C008221, // (0x021 | ACT_FLAG_STATIONARY | ACT_FLAG_SHORT_HITBOX | ACT_FLAG_ALLOW_FIRST_PERSON | ACT_FLAG_PAUSE_EXIT)
        ACT_STOP_CROUCHING =              0x0C008222, // (0x022 | ACT_FLAG_STATIONARY | ACT_FLAG_SHORT_HITBOX | ACT_FLAG_ALLOW_FIRST_PERSON | ACT_FLAG_PAUSE_EXIT)
        ACT_START_CRAWLING =              0x0C008223, // (0x023 | ACT_FLAG_STATIONARY | ACT_FLAG_SHORT_HITBOX | ACT_FLAG_ALLOW_FIRST_PERSON | ACT_FLAG_PAUSE_EXIT)
        ACT_STOP_CRAWLING =               0x0C008224, // (0x024 | ACT_FLAG_STATIONARY | ACT_FLAG_SHORT_HITBOX | ACT_FLAG_ALLOW_FIRST_PERSON | ACT_FLAG_PAUSE_EXIT)
        ACT_SLIDE_KICK_SLIDE_STOP =       0x08000225, // (0x025 | ACT_FLAG_STATIONARY | ACT_FLAG_PAUSE_EXIT)
        ACT_SHOCKWAVE_BOUNCE =            0x00020226, // (0x026 | ACT_FLAG_STATIONARY | ACT_FLAG_INVULNERABLE)
        ACT_FIRST_PERSON =                0x0C000227, // (0x027 | ACT_FLAG_STATIONARY | ACT_FLAG_ALLOW_FIRST_PERSON | ACT_FLAG_PAUSE_EXIT)
        ACT_BACKFLIP_LAND_STOP =          0x0800022F, // (0x02F | ACT_FLAG_STATIONARY | ACT_FLAG_PAUSE_EXIT)
        ACT_JUMP_LAND_STOP =              0x0C000230, // (0x030 | ACT_FLAG_STATIONARY | ACT_FLAG_ALLOW_FIRST_PERSON | ACT_FLAG_PAUSE_EXIT)
        ACT_DOUBLE_JUMP_LAND_STOP =       0x0C000231, // (0x031 | ACT_FLAG_STATIONARY | ACT_FLAG_ALLOW_FIRST_PERSON | ACT_FLAG_PAUSE_EXIT)
        ACT_FREEFALL_LAND_STOP =          0x0C000232, // (0x032 | ACT_FLAG_STATIONARY | ACT_FLAG_ALLOW_FIRST_PERSON | ACT_FLAG_PAUSE_EXIT)
        ACT_SIDE_FLIP_LAND_STOP =         0x0C000233, // (0x033 | ACT_FLAG_STATIONARY | ACT_FLAG_ALLOW_FIRST_PERSON | ACT_FLAG_PAUSE_EXIT)
        ACT_HOLD_JUMP_LAND_STOP =         0x08000234, // (0x034 | ACT_FLAG_STATIONARY | ACT_FLAG_PAUSE_EXIT)
        ACT_HOLD_FREEFALL_LAND_STOP =     0x08000235, // (0x035 | ACT_FLAG_STATIONARY | ACT_FLAG_PAUSE_EXIT)
        ACT_AIR_THROW_LAND =              0x80000A36, // (0x036 | ACT_FLAG_STATIONARY | ACT_FLAG_AIR | ACT_FLAG_THROWING)
        ACT_TWIRL_LAND =                  0x18800238, // (0x038 | ACT_FLAG_STATIONARY | ACT_FLAG_ATTACKING | ACT_FLAG_PAUSE_EXIT | ACT_FLAG_SWIMMING_OR_FLYING)
        ACT_LAVA_BOOST_LAND =             0x08000239, // (0x039 | ACT_FLAG_STATIONARY | ACT_FLAG_PAUSE_EXIT)
        ACT_TRIPLE_JUMP_LAND_STOP =       0x0800023A, // (0x03A | ACT_FLAG_STATIONARY | ACT_FLAG_PAUSE_EXIT)
        ACT_LONG_JUMP_LAND_STOP =         0x0800023B, // (0x03B | ACT_FLAG_STATIONARY | ACT_FLAG_PAUSE_EXIT)
        ACT_GROUND_POUND_LAND =           0x0080023C, // (0x03C | ACT_FLAG_STATIONARY | ACT_FLAG_ATTACKING)
        ACT_BRAKING_STOP =                0x0C00023D, // (0x03D | ACT_FLAG_STATIONARY | ACT_FLAG_ALLOW_FIRST_PERSON | ACT_FLAG_PAUSE_EXIT)
        ACT_BUTT_SLIDE_STOP =             0x0C00023E, // (0x03E | ACT_FLAG_STATIONARY | ACT_FLAG_ALLOW_FIRST_PERSON | ACT_FLAG_PAUSE_EXIT)
        ACT_HOLD_BUTT_SLIDE_STOP =        0x0800043F, // (0x03F | ACT_FLAG_MOVING | ACT_FLAG_PAUSE_EXIT)

        // group 0x040: moving (ground) actions
        ACT_WALKING =                     0x04000440, // (0x040 | ACT_FLAG_MOVING | ACT_FLAG_ALLOW_FIRST_PERSON)
        ACT_HOLD_WALKING =                0x00000442, // (0x042 | ACT_FLAG_MOVING)
        ACT_TURNING_AROUND =              0x00000443, // (0x043 | ACT_FLAG_MOVING)
        ACT_FINISH_TURNING_AROUND =       0x00000444, // (0x044 | ACT_FLAG_MOVING)
        ACT_BRAKING =                     0x04000445, // (0x045 | ACT_FLAG_MOVING | ACT_FLAG_ALLOW_FIRST_PERSON)
        ACT_RIDING_SHELL_GROUND =         0x20810446, // (0x046 | ACT_FLAG_MOVING | ACT_FLAG_RIDING_SHELL | ACT_FLAG_ATTACKING | ACT_FLAG_WATER_OR_TEXT)
        ACT_HOLD_HEAVY_WALKING =          0x00000447, // (0x047 | ACT_FLAG_MOVING)
        ACT_CRAWLING =                    0x04008448, // (0x048 | ACT_FLAG_MOVING | ACT_FLAG_SHORT_HITBOX | ACT_FLAG_ALLOW_FIRST_PERSON)
        ACT_BURNING_GROUND =              0x00020449, // (0x049 | ACT_FLAG_MOVING | ACT_FLAG_INVULNERABLE)
        ACT_DECELERATING =                0x0400044A, // (0x04A | ACT_FLAG_MOVING | ACT_FLAG_ALLOW_FIRST_PERSON)
        ACT_HOLD_DECELERATING =           0x0000044B, // (0x04B | ACT_FLAG_MOVING)
        ACT_BEGIN_SLIDING =               0x00000050, // (0x050)
        ACT_HOLD_BEGIN_SLIDING =          0x00000051, // (0x051)
        ACT_BUTT_SLIDE =                  0x00840452, // (0x052 | ACT_FLAG_MOVING | ACT_FLAG_BUTT_OR_STOMACH_SLIDE | ACT_FLAG_ATTACKING)
        ACT_STOMACH_SLIDE =               0x008C0453, // (0x053 | ACT_FLAG_MOVING | ACT_FLAG_BUTT_OR_STOMACH_SLIDE | ACT_FLAG_DIVING | ACT_FLAG_ATTACKING)
        ACT_HOLD_BUTT_SLIDE =             0x00840454, // (0x054 | ACT_FLAG_MOVING | ACT_FLAG_BUTT_OR_STOMACH_SLIDE | ACT_FLAG_ATTACKING)
        ACT_HOLD_STOMACH_SLIDE =          0x008C0455, // (0x055 | ACT_FLAG_MOVING | ACT_FLAG_BUTT_OR_STOMACH_SLIDE | ACT_FLAG_DIVING | ACT_FLAG_ATTACKING)
        ACT_DIVE_SLIDE =                  0x00880456, // (0x056 | ACT_FLAG_MOVING | ACT_FLAG_DIVING | ACT_FLAG_ATTACKING)
        ACT_MOVE_PUNCHING =               0x00800457, // (0x057 | ACT_FLAG_MOVING | ACT_FLAG_ATTACKING)
        ACT_CROUCH_SLIDE =                0x04808459, // (0x059 | ACT_FLAG_MOVING | ACT_FLAG_SHORT_HITBOX | ACT_FLAG_ATTACKING | ACT_FLAG_ALLOW_FIRST_PERSON)
        ACT_SLIDE_KICK_SLIDE =            0x0080045A, // (0x05A | ACT_FLAG_MOVING | ACT_FLAG_ATTACKING)
        ACT_HARD_BACKWARD_GROUND_KB =     0x00020460, // (0x060 | ACT_FLAG_MOVING | ACT_FLAG_INVULNERABLE)
        ACT_HARD_FORWARD_GROUND_KB =      0x00020461, // (0x061 | ACT_FLAG_MOVING | ACT_FLAG_INVULNERABLE)
        ACT_BACKWARD_GROUND_KB =          0x00020462, // (0x062 | ACT_FLAG_MOVING | ACT_FLAG_INVULNERABLE)
        ACT_FORWARD_GROUND_KB =           0x00020463, // (0x063 | ACT_FLAG_MOVING | ACT_FLAG_INVULNERABLE)
        ACT_SOFT_BACKWARD_GROUND_KB =     0x00020464, // (0x064 | ACT_FLAG_MOVING | ACT_FLAG_INVULNERABLE)
        ACT_SOFT_FORWARD_GROUND_KB =      0x00020465, // (0x065 | ACT_FLAG_MOVING | ACT_FLAG_INVULNERABLE)
        ACT_GROUND_BONK =                 0x00020466, // (0x066 | ACT_FLAG_MOVING | ACT_FLAG_INVULNERABLE)
        ACT_DEATH_EXIT_LAND =             0x00020467, // (0x067 | ACT_FLAG_MOVING | ACT_FLAG_INVULNERABLE)
        ACT_JUMP_LAND =                   0x04000470, // (0x070 | ACT_FLAG_MOVING | ACT_FLAG_ALLOW_FIRST_PERSON)
        ACT_FREEFALL_LAND =               0x04000471, // (0x071 | ACT_FLAG_MOVING | ACT_FLAG_ALLOW_FIRST_PERSON)
        ACT_DOUBLE_JUMP_LAND =            0x04000472, // (0x072 | ACT_FLAG_MOVING | ACT_FLAG_ALLOW_FIRST_PERSON)
        ACT_SIDE_FLIP_LAND =              0x04000473, // (0x073 | ACT_FLAG_MOVING | ACT_FLAG_ALLOW_FIRST_PERSON)
        ACT_HOLD_JUMP_LAND =              0x00000474, // (0x074 | ACT_FLAG_MOVING)
        ACT_HOLD_FREEFALL_LAND =          0x00000475, // (0x075 | ACT_FLAG_MOVING)
        ACT_QUICKSAND_JUMP_LAND =         0x00000476, // (0x076 | ACT_FLAG_MOVING)
        ACT_HOLD_QUICKSAND_JUMP_LAND =    0x00000477, // (0x077 | ACT_FLAG_MOVING)
        ACT_TRIPLE_JUMP_LAND =            0x04000478, // (0x078 | ACT_FLAG_MOVING | ACT_FLAG_ALLOW_FIRST_PERSON)
        ACT_LONG_JUMP_LAND =              0x00000479, // (0x079 | ACT_FLAG_MOVING)
        ACT_BACKFLIP_LAND =               0x0400047A, // (0x07A | ACT_FLAG_MOVING | ACT_FLAG_ALLOW_FIRST_PERSON)

        // group 0x080: airborne actions
        ACT_JUMP =                        0x03000880, // (0x080 | ACT_FLAG_AIR | ACT_FLAG_ALLOW_VERTICAL_WIND_ACTION | ACT_FLAG_CONTROL_JUMP_HEIGHT)
        ACT_DOUBLE_JUMP =                 0x03000881, // (0x081 | ACT_FLAG_AIR | ACT_FLAG_ALLOW_VERTICAL_WIND_ACTION | ACT_FLAG_CONTROL_JUMP_HEIGHT)
        ACT_TRIPLE_JUMP =                 0x01000882, // (0x082 | ACT_FLAG_AIR | ACT_FLAG_ALLOW_VERTICAL_WIND_ACTION)
        ACT_BACKFLIP =                    0x01000883, // (0x083 | ACT_FLAG_AIR | ACT_FLAG_ALLOW_VERTICAL_WIND_ACTION)
        ACT_STEEP_JUMP =                  0x03000885, // (0x085 | ACT_FLAG_AIR | ACT_FLAG_ALLOW_VERTICAL_WIND_ACTION | ACT_FLAG_CONTROL_JUMP_HEIGHT)
        ACT_WALL_KICK_AIR =               0x03000886, // (0x086 | ACT_FLAG_AIR | ACT_FLAG_ALLOW_VERTICAL_WIND_ACTION | ACT_FLAG_CONTROL_JUMP_HEIGHT)
        ACT_SIDE_FLIP =                   0x01000887, // (0x087 | ACT_FLAG_AIR | ACT_FLAG_ALLOW_VERTICAL_WIND_ACTION)
        ACT_LONG_JUMP =                   0x03000888, // (0x088 | ACT_FLAG_AIR | ACT_FLAG_ALLOW_VERTICAL_WIND_ACTION | ACT_FLAG_CONTROL_JUMP_HEIGHT)
        ACT_WATER_JUMP =                  0x01000889, // (0x089 | ACT_FLAG_AIR | ACT_FLAG_ALLOW_VERTICAL_WIND_ACTION)
        ACT_DIVE =                        0x0188088A, // (0x08A | ACT_FLAG_AIR | ACT_FLAG_DIVING | ACT_FLAG_ATTACKING | ACT_FLAG_ALLOW_VERTICAL_WIND_ACTION)
        ACT_FREEFALL =                    0x0100088C, // (0x08C | ACT_FLAG_AIR | ACT_FLAG_ALLOW_VERTICAL_WIND_ACTION)
        ACT_TOP_OF_POLE_JUMP =            0x0300088D, // (0x08D | ACT_FLAG_AIR | ACT_FLAG_ALLOW_VERTICAL_WIND_ACTION | ACT_FLAG_CONTROL_JUMP_HEIGHT)
        ACT_BUTT_SLIDE_AIR =              0x0300088E, // (0x08E | ACT_FLAG_AIR | ACT_FLAG_ALLOW_VERTICAL_WIND_ACTION | ACT_FLAG_CONTROL_JUMP_HEIGHT)
        ACT_FLYING_TRIPLE_JUMP =          0x03000894, // (0x094 | ACT_FLAG_AIR | ACT_FLAG_ALLOW_VERTICAL_WIND_ACTION | ACT_FLAG_CONTROL_JUMP_HEIGHT)
        ACT_SHOT_FROM_CANNON =            0x00880898, // (0x098 | ACT_FLAG_AIR | ACT_FLAG_DIVING | ACT_FLAG_ATTACKING)
        ACT_FLYING =                      0x10880899, // (0x099 | ACT_FLAG_AIR | ACT_FLAG_DIVING | ACT_FLAG_ATTACKING | ACT_FLAG_SWIMMING_OR_FLYING)
        ACT_RIDING_SHELL_JUMP =           0x0281089A, // (0x09A | ACT_FLAG_AIR | ACT_FLAG_RIDING_SHELL | ACT_FLAG_ATTACKING | ACT_FLAG_CONTROL_JUMP_HEIGHT)
        ACT_RIDING_SHELL_FALL =           0x0081089B, // (0x09B | ACT_FLAG_AIR | ACT_FLAG_RIDING_SHELL | ACT_FLAG_ATTACKING)
        ACT_VERTICAL_WIND =               0x1008089C, // (0x09C | ACT_FLAG_AIR | ACT_FLAG_DIVING | ACT_FLAG_SWIMMING_OR_FLYING)
        ACT_HOLD_JUMP =                   0x030008A0, // (0x0A0 | ACT_FLAG_AIR | ACT_FLAG_ALLOW_VERTICAL_WIND_ACTION | ACT_FLAG_CONTROL_JUMP_HEIGHT)
        ACT_HOLD_FREEFALL =               0x010008A1, // (0x0A1 | ACT_FLAG_AIR | ACT_FLAG_ALLOW_VERTICAL_WIND_ACTION)
        ACT_HOLD_BUTT_SLIDE_AIR =         0x010008A2, // (0x0A2 | ACT_FLAG_AIR | ACT_FLAG_ALLOW_VERTICAL_WIND_ACTION)
        ACT_HOLD_WATER_JUMP =             0x010008A3, // (0x0A3 | ACT_FLAG_AIR | ACT_FLAG_ALLOW_VERTICAL_WIND_ACTION)
        ACT_TWIRLING =                    0x108008A4, // (0x0A4 | ACT_FLAG_AIR | ACT_FLAG_ATTACKING | ACT_FLAG_SWIMMING_OR_FLYING)
        ACT_FORWARD_ROLLOUT =             0x010008A6, // (0x0A6 | ACT_FLAG_AIR | ACT_FLAG_ALLOW_VERTICAL_WIND_ACTION)
        ACT_AIR_HIT_WALL =                0x000008A7, // (0x0A7 | ACT_FLAG_AIR)
        ACT_RIDING_HOOT =                 0x000004A8, // (0x0A8 | ACT_FLAG_MOVING)
        ACT_GROUND_POUND =                0x008008A9, // (0x0A9 | ACT_FLAG_AIR | ACT_FLAG_ATTACKING)
        ACT_SLIDE_KICK =                  0x018008AA, // (0x0AA | ACT_FLAG_AIR | ACT_FLAG_ATTACKING | ACT_FLAG_ALLOW_VERTICAL_WIND_ACTION)
        ACT_AIR_THROW =                   0x830008AB, // (0x0AB | ACT_FLAG_AIR | ACT_FLAG_ALLOW_VERTICAL_WIND_ACTION | ACT_FLAG_CONTROL_JUMP_HEIGHT | ACT_FLAG_THROWING)
        ACT_JUMP_KICK =                   0x018008AC, // (0x0AC | ACT_FLAG_AIR | ACT_FLAG_ATTACKING | ACT_FLAG_ALLOW_VERTICAL_WIND_ACTION)
        ACT_BACKWARD_ROLLOUT =            0x010008AD, // (0x0AD | ACT_FLAG_AIR | ACT_FLAG_ALLOW_VERTICAL_WIND_ACTION)
        ACT_CRAZY_BOX_BOUNCE =            0x000008AE, // (0x0AE | ACT_FLAG_AIR)
        ACT_SPECIAL_TRIPLE_JUMP =         0x030008AF, // (0x0AF | ACT_FLAG_AIR | ACT_FLAG_ALLOW_VERTICAL_WIND_ACTION | ACT_FLAG_CONTROL_JUMP_HEIGHT)
        ACT_BACKWARD_AIR_KB =             0x010208B0, // (0x0B0 | ACT_FLAG_AIR | ACT_FLAG_INVULNERABLE | ACT_FLAG_ALLOW_VERTICAL_WIND_ACTION)
        ACT_FORWARD_AIR_KB =              0x010208B1, // (0x0B1 | ACT_FLAG_AIR | ACT_FLAG_INVULNERABLE | ACT_FLAG_ALLOW_VERTICAL_WIND_ACTION)
        ACT_HARD_FORWARD_AIR_KB =         0x010208B2, // (0x0B2 | ACT_FLAG_AIR | ACT_FLAG_INVULNERABLE | ACT_FLAG_ALLOW_VERTICAL_WIND_ACTION)
        ACT_HARD_BACKWARD_AIR_KB =        0x010208B3, // (0x0B3 | ACT_FLAG_AIR | ACT_FLAG_INVULNERABLE | ACT_FLAG_ALLOW_VERTICAL_WIND_ACTION)
        ACT_BURNING_JUMP =                0x010208B4, // (0x0B4 | ACT_FLAG_AIR | ACT_FLAG_INVULNERABLE | ACT_FLAG_ALLOW_VERTICAL_WIND_ACTION)
        ACT_BURNING_FALL =                0x010208B5, // (0x0B5 | ACT_FLAG_AIR | ACT_FLAG_INVULNERABLE | ACT_FLAG_ALLOW_VERTICAL_WIND_ACTION)
        ACT_SOFT_BONK =                   0x010208B6, // (0x0B6 | ACT_FLAG_AIR | ACT_FLAG_INVULNERABLE | ACT_FLAG_ALLOW_VERTICAL_WIND_ACTION)
        ACT_LAVA_BOOST =                  0x010208B7, // (0x0B7 | ACT_FLAG_AIR | ACT_FLAG_INVULNERABLE | ACT_FLAG_ALLOW_VERTICAL_WIND_ACTION)
        ACT_GETTING_BLOWN =               0x010208B8, // (0x0B8 | ACT_FLAG_AIR | ACT_FLAG_INVULNERABLE | ACT_FLAG_ALLOW_VERTICAL_WIND_ACTION)
        ACT_THROWN_FORWARD =              0x010208BD, // (0x0BD | ACT_FLAG_AIR | ACT_FLAG_INVULNERABLE | ACT_FLAG_ALLOW_VERTICAL_WIND_ACTION)
        ACT_THROWN_BACKWARD =             0x010208BE, // (0x0BE | ACT_FLAG_AIR | ACT_FLAG_INVULNERABLE | ACT_FLAG_ALLOW_VERTICAL_WIND_ACTION)

        // group 0x0C0: submerged actions
        ACT_WATER_IDLE =                  0x380022C0, // (0x0C0 | ACT_FLAG_STATIONARY | ACT_FLAG_SWIMMING | ACT_FLAG_PAUSE_EXIT | ACT_FLAG_SWIMMING_OR_FLYING | ACT_FLAG_WATER_OR_TEXT)
        ACT_HOLD_WATER_IDLE =             0x380022C1, // (0x0C1 | ACT_FLAG_STATIONARY | ACT_FLAG_SWIMMING | ACT_FLAG_PAUSE_EXIT | ACT_FLAG_SWIMMING_OR_FLYING | ACT_FLAG_WATER_OR_TEXT)
        ACT_WATER_ACTION_END =            0x300022C2, // (0x0C2 | ACT_FLAG_STATIONARY | ACT_FLAG_SWIMMING | ACT_FLAG_SWIMMING_OR_FLYING | ACT_FLAG_WATER_OR_TEXT)
        ACT_HOLD_WATER_ACTION_END =       0x300022C3, // (0x0C3 | ACT_FLAG_STATIONARY | ACT_FLAG_SWIMMING | ACT_FLAG_SWIMMING_OR_FLYING | ACT_FLAG_WATER_OR_TEXT)
        ACT_DROWNING =                    0x300032C4, // (0x0C4 | ACT_FLAG_STATIONARY | ACT_FLAG_INTANGIBLE | ACT_FLAG_SWIMMING | ACT_FLAG_SWIMMING_OR_FLYING | ACT_FLAG_WATER_OR_TEXT)
        ACT_BACKWARD_WATER_KB =           0x300222C5, // (0x0C5 | ACT_FLAG_STATIONARY | ACT_FLAG_SWIMMING | ACT_FLAG_INVULNERABLE | ACT_FLAG_SWIMMING_OR_FLYING | ACT_FLAG_WATER_OR_TEXT)
        ACT_FORWARD_WATER_KB =            0x300222C6, // (0x0C6 | ACT_FLAG_STATIONARY | ACT_FLAG_SWIMMING | ACT_FLAG_INVULNERABLE | ACT_FLAG_SWIMMING_OR_FLYING | ACT_FLAG_WATER_OR_TEXT)
        ACT_WATER_DEATH =                 0x300032C7, // (0x0C7 | ACT_FLAG_STATIONARY | ACT_FLAG_INTANGIBLE | ACT_FLAG_SWIMMING | ACT_FLAG_SWIMMING_OR_FLYING | ACT_FLAG_WATER_OR_TEXT)
        ACT_WATER_SHOCKED =               0x300222C8, // (0x0C8 | ACT_FLAG_STATIONARY | ACT_FLAG_SWIMMING | ACT_FLAG_INVULNERABLE | ACT_FLAG_SWIMMING_OR_FLYING | ACT_FLAG_WATER_OR_TEXT)
        ACT_BREASTSTROKE =                0x300024D0, // (0x0D0 | ACT_FLAG_MOVING | ACT_FLAG_SWIMMING | ACT_FLAG_SWIMMING_OR_FLYING | ACT_FLAG_WATER_OR_TEXT)
        ACT_SWIMMING_END =                0x300024D1, // (0x0D1 | ACT_FLAG_MOVING | ACT_FLAG_SWIMMING | ACT_FLAG_SWIMMING_OR_FLYING | ACT_FLAG_WATER_OR_TEXT)
        ACT_FLUTTER_KICK =                0x300024D2, // (0x0D2 | ACT_FLAG_MOVING | ACT_FLAG_SWIMMING | ACT_FLAG_SWIMMING_OR_FLYING | ACT_FLAG_WATER_OR_TEXT)
        ACT_HOLD_BREASTSTROKE =           0x300024D3, // (0x0D3 | ACT_FLAG_MOVING | ACT_FLAG_SWIMMING | ACT_FLAG_SWIMMING_OR_FLYING | ACT_FLAG_WATER_OR_TEXT)
        ACT_HOLD_SWIMMING_END =           0x300024D4, // (0x0D4 | ACT_FLAG_MOVING | ACT_FLAG_SWIMMING | ACT_FLAG_SWIMMING_OR_FLYING | ACT_FLAG_WATER_OR_TEXT)
        ACT_HOLD_FLUTTER_KICK =           0x300024D5, // (0x0D5 | ACT_FLAG_MOVING | ACT_FLAG_SWIMMING | ACT_FLAG_SWIMMING_OR_FLYING | ACT_FLAG_WATER_OR_TEXT)
        ACT_WATER_SHELL_SWIMMING =        0x300024D6, // (0x0D6 | ACT_FLAG_MOVING | ACT_FLAG_SWIMMING | ACT_FLAG_SWIMMING_OR_FLYING | ACT_FLAG_WATER_OR_TEXT)
        ACT_WATER_THROW =                 0x300024E0, // (0x0E0 | ACT_FLAG_MOVING | ACT_FLAG_SWIMMING | ACT_FLAG_SWIMMING_OR_FLYING | ACT_FLAG_WATER_OR_TEXT)
        ACT_WATER_PUNCH =                 0x300024E1, // (0x0E1 | ACT_FLAG_MOVING | ACT_FLAG_SWIMMING | ACT_FLAG_SWIMMING_OR_FLYING | ACT_FLAG_WATER_OR_TEXT)
        ACT_WATER_PLUNGE =                0x300022E2, // (0x0E2 | ACT_FLAG_STATIONARY | ACT_FLAG_SWIMMING | ACT_FLAG_SWIMMING_OR_FLYING | ACT_FLAG_WATER_OR_TEXT)
        ACT_CAUGHT_IN_WHIRLPOOL =         0x300222E3, // (0x0E3 | ACT_FLAG_STATIONARY | ACT_FLAG_SWIMMING | ACT_FLAG_INVULNERABLE | ACT_FLAG_SWIMMING_OR_FLYING | ACT_FLAG_WATER_OR_TEXT)
        ACT_METAL_WATER_STANDING =        0x080042F0, // (0x0F0 | ACT_FLAG_STATIONARY | ACT_FLAG_METAL_WATER | ACT_FLAG_PAUSE_EXIT)
        ACT_HOLD_METAL_WATER_STANDING =   0x080042F1, // (0x0F1 | ACT_FLAG_STATIONARY | ACT_FLAG_METAL_WATER | ACT_FLAG_PAUSE_EXIT)
        ACT_METAL_WATER_WALKING =         0x000044F2, // (0x0F2 | ACT_FLAG_MOVING | ACT_FLAG_METAL_WATER)
        ACT_HOLD_METAL_WATER_WALKING =    0x000044F3, // (0x0F3 | ACT_FLAG_MOVING | ACT_FLAG_METAL_WATER)
        ACT_METAL_WATER_FALLING =         0x000042F4, // (0x0F4 | ACT_FLAG_STATIONARY | ACT_FLAG_METAL_WATER)
        ACT_HOLD_METAL_WATER_FALLING =    0x000042F5, // (0x0F5 | ACT_FLAG_STATIONARY | ACT_FLAG_METAL_WATER)
        ACT_METAL_WATER_FALL_LAND =       0x000042F6, // (0x0F6 | ACT_FLAG_STATIONARY | ACT_FLAG_METAL_WATER)
        ACT_HOLD_METAL_WATER_FALL_LAND =  0x000042F7, // (0x0F7 | ACT_FLAG_STATIONARY | ACT_FLAG_METAL_WATER)
        ACT_METAL_WATER_JUMP =            0x000044F8, // (0x0F8 | ACT_FLAG_MOVING | ACT_FLAG_METAL_WATER)
        ACT_HOLD_METAL_WATER_JUMP =       0x000044F9, // (0x0F9 | ACT_FLAG_MOVING | ACT_FLAG_METAL_WATER)
        ACT_METAL_WATER_JUMP_LAND =       0x000044FA, // (0x0FA | ACT_FLAG_MOVING | ACT_FLAG_METAL_WATER)
        ACT_HOLD_METAL_WATER_JUMP_LAND =  0x000044FB, // (0x0FB | ACT_FLAG_MOVING | ACT_FLAG_METAL_WATER)

        // group 0x100: cutscene actions
        ACT_DISAPPEARED =                 0x00001300, // (0x100 | ACT_FLAG_STATIONARY | ACT_FLAG_INTANGIBLE)
        ACT_INTRO_CUTSCENE =              0x04001301, // (0x101 | ACT_FLAG_STATIONARY | ACT_FLAG_INTANGIBLE | ACT_FLAG_ALLOW_FIRST_PERSON)
        ACT_STAR_DANCE_EXIT =             0x00001302, // (0x102 | ACT_FLAG_STATIONARY | ACT_FLAG_INTANGIBLE)
        ACT_STAR_DANCE_WATER =            0x00001303, // (0x103 | ACT_FLAG_STATIONARY | ACT_FLAG_INTANGIBLE)
        ACT_FALL_AFTER_STAR_GRAB =        0x00001904, // (0x104 | ACT_FLAG_AIR | ACT_FLAG_INTANGIBLE)
        ACT_READING_AUTOMATIC_DIALOG =    0x20001305, // (0x105 | ACT_FLAG_STATIONARY | ACT_FLAG_INTANGIBLE | ACT_FLAG_WATER_OR_TEXT)
        ACT_READING_NPC_DIALOG =          0x20001306, // (0x106 | ACT_FLAG_STATIONARY | ACT_FLAG_INTANGIBLE | ACT_FLAG_WATER_OR_TEXT)
        ACT_STAR_DANCE_NO_EXIT =          0x00001307, // (0x107 | ACT_FLAG_STATIONARY | ACT_FLAG_INTANGIBLE)
        ACT_READING_SIGN =                0x00001308, // (0x108 | ACT_FLAG_STATIONARY | ACT_FLAG_INTANGIBLE)
        ACT_JUMBO_STAR_CUTSCENE =         0x00001909, // (0x109 | ACT_FLAG_AIR | ACT_FLAG_INTANGIBLE)
        ACT_WAITING_FOR_DIALOG =          0x0000130A, // (0x10A | ACT_FLAG_STATIONARY | ACT_FLAG_INTANGIBLE)
        ACT_DEBUG_FREE_MOVE =             0x0000130F, // (0x10F | ACT_FLAG_STATIONARY | ACT_FLAG_INTANGIBLE)
        ACT_STANDING_DEATH =              0x00021311, // (0x111 | ACT_FLAG_STATIONARY | ACT_FLAG_INTANGIBLE | ACT_FLAG_INVULNERABLE)
        ACT_QUICKSAND_DEATH =             0x00021312, // (0x112 | ACT_FLAG_STATIONARY | ACT_FLAG_INTANGIBLE | ACT_FLAG_INVULNERABLE)
        ACT_ELECTROCUTION =               0x00021313, // (0x113 | ACT_FLAG_STATIONARY | ACT_FLAG_INTANGIBLE | ACT_FLAG_INVULNERABLE)
        ACT_SUFFOCATION =                 0x00021314, // (0x114 | ACT_FLAG_STATIONARY | ACT_FLAG_INTANGIBLE | ACT_FLAG_INVULNERABLE)
        ACT_DEATH_ON_STOMACH =            0x00021315, // (0x115 | ACT_FLAG_STATIONARY | ACT_FLAG_INTANGIBLE | ACT_FLAG_INVULNERABLE)
        ACT_DEATH_ON_BACK =               0x00021316, // (0x116 | ACT_FLAG_STATIONARY | ACT_FLAG_INTANGIBLE | ACT_FLAG_INVULNERABLE)
        ACT_EATEN_BY_BUBBA =              0x00021317, // (0x117 | ACT_FLAG_STATIONARY | ACT_FLAG_INTANGIBLE | ACT_FLAG_INVULNERABLE)
        ACT_END_PEACH_CUTSCENE =          0x00001918, // (0x118 | ACT_FLAG_AIR | ACT_FLAG_INTANGIBLE)
        ACT_CREDITS_CUTSCENE =            0x00001319, // (0x119 | ACT_FLAG_STATIONARY | ACT_FLAG_INTANGIBLE)
        ACT_END_WAVING_CUTSCENE =         0x0000131A, // (0x11A | ACT_FLAG_STATIONARY | ACT_FLAG_INTANGIBLE)
        ACT_PULLING_DOOR =                0x00001320, // (0x120 | ACT_FLAG_STATIONARY | ACT_FLAG_INTANGIBLE)
        ACT_PUSHING_DOOR =                0x00001321, // (0x121 | ACT_FLAG_STATIONARY | ACT_FLAG_INTANGIBLE)
        ACT_WARP_DOOR_SPAWN =             0x00001322, // (0x122 | ACT_FLAG_STATIONARY | ACT_FLAG_INTANGIBLE)
        ACT_EMERGE_FROM_PIPE =            0x00001923, // (0x123 | ACT_FLAG_AIR | ACT_FLAG_INTANGIBLE)
        ACT_SPAWN_SPIN_AIRBORNE =         0x00001924, // (0x124 | ACT_FLAG_AIR | ACT_FLAG_INTANGIBLE)
        ACT_SPAWN_SPIN_LANDING =          0x00001325, // (0x125 | ACT_FLAG_STATIONARY | ACT_FLAG_INTANGIBLE)
        ACT_EXIT_AIRBORNE =               0x00001926, // (0x126 | ACT_FLAG_AIR | ACT_FLAG_INTANGIBLE)
        ACT_EXIT_LAND_SAVE_DIALOG =       0x00001327, // (0x127 | ACT_FLAG_STATIONARY | ACT_FLAG_INTANGIBLE)
        ACT_DEATH_EXIT =                  0x00001928, // (0x128 | ACT_FLAG_AIR | ACT_FLAG_INTANGIBLE)
        ACT_UNUSED_DEATH_EXIT =           0x00001929, // (0x129 | ACT_FLAG_AIR | ACT_FLAG_INTANGIBLE)
        ACT_FALLING_DEATH_EXIT =          0x0000192A, // (0x12A | ACT_FLAG_AIR | ACT_FLAG_INTANGIBLE)
        ACT_SPECIAL_EXIT_AIRBORNE =       0x0000192B, // (0x12B | ACT_FLAG_AIR | ACT_FLAG_INTANGIBLE)
        ACT_SPECIAL_DEATH_EXIT =          0x0000192C, // (0x12C | ACT_FLAG_AIR | ACT_FLAG_INTANGIBLE)
        ACT_FALLING_EXIT_AIRBORNE =       0x0000192D, // (0x12D | ACT_FLAG_AIR | ACT_FLAG_INTANGIBLE)
        ACT_UNLOCKING_KEY_DOOR =          0x0000132E, // (0x12E | ACT_FLAG_STATIONARY | ACT_FLAG_INTANGIBLE)
        ACT_UNLOCKING_STAR_DOOR =         0x0000132F, // (0x12F | ACT_FLAG_STATIONARY | ACT_FLAG_INTANGIBLE)
        ACT_ENTERING_STAR_DOOR =          0x00001331, // (0x131 | ACT_FLAG_STATIONARY | ACT_FLAG_INTANGIBLE)
        ACT_SPAWN_NO_SPIN_AIRBORNE =      0x00001932, // (0x132 | ACT_FLAG_AIR | ACT_FLAG_INTANGIBLE)
        ACT_SPAWN_NO_SPIN_LANDING =       0x00001333, // (0x133 | ACT_FLAG_STATIONARY | ACT_FLAG_INTANGIBLE)
        ACT_BBH_ENTER_JUMP =              0x00001934, // (0x134 | ACT_FLAG_AIR | ACT_FLAG_INTANGIBLE)
        ACT_BBH_ENTER_SPIN =              0x00001535, // (0x135 | ACT_FLAG_MOVING | ACT_FLAG_INTANGIBLE)
        ACT_TELEPORT_FADE_OUT =           0x00001336, // (0x136 | ACT_FLAG_STATIONARY | ACT_FLAG_INTANGIBLE)
        ACT_TELEPORT_FADE_IN =            0x00001337, // (0x137 | ACT_FLAG_STATIONARY | ACT_FLAG_INTANGIBLE)
        ACT_SHOCKED =                     0x00020338, // (0x138 | ACT_FLAG_STATIONARY | ACT_FLAG_INVULNERABLE)
        ACT_SQUISHED =                    0x00020339, // (0x139 | ACT_FLAG_STATIONARY | ACT_FLAG_INVULNERABLE)
        ACT_HEAD_STUCK_IN_GROUND =        0x0002033A, // (0x13A | ACT_FLAG_STATIONARY | ACT_FLAG_INVULNERABLE)
        ACT_BUTT_STUCK_IN_GROUND =        0x0002033B, // (0x13B | ACT_FLAG_STATIONARY | ACT_FLAG_INVULNERABLE)
        ACT_FEET_STUCK_IN_GROUND =        0x0002033C, // (0x13C | ACT_FLAG_STATIONARY | ACT_FLAG_INVULNERABLE)
        ACT_PUTTING_ON_CAP =              0x0000133D, // (0x13D | ACT_FLAG_STATIONARY | ACT_FLAG_INTANGIBLE)

        // group 0x140: "automatic" actions
        ACT_HOLDING_POLE =                0x08100340, // (0x140 | ACT_FLAG_STATIONARY | ACT_FLAG_ON_POLE | ACT_FLAG_PAUSE_EXIT)
        ACT_GRAB_POLE_SLOW =              0x00100341, // (0x141 | ACT_FLAG_STATIONARY | ACT_FLAG_ON_POLE)
        ACT_GRAB_POLE_FAST =              0x00100342, // (0x142 | ACT_FLAG_STATIONARY | ACT_FLAG_ON_POLE)
        ACT_CLIMBING_POLE =               0x00100343, // (0x143 | ACT_FLAG_STATIONARY | ACT_FLAG_ON_POLE)
        ACT_TOP_OF_POLE_TRANSITION =      0x00100344, // (0x144 | ACT_FLAG_STATIONARY | ACT_FLAG_ON_POLE)
        ACT_TOP_OF_POLE =                 0x00100345, // (0x145 | ACT_FLAG_STATIONARY | ACT_FLAG_ON_POLE)
        ACT_START_HANGING =               0x08200348, // (0x148 | ACT_FLAG_STATIONARY | ACT_FLAG_HANGING | ACT_FLAG_PAUSE_EXIT)
        ACT_HANGING =                     0x00200349, // (0x149 | ACT_FLAG_STATIONARY | ACT_FLAG_HANGING)
        ACT_HANG_MOVING =                 0x0020054A, // (0x14A | ACT_FLAG_MOVING | ACT_FLAG_HANGING)
        ACT_LEDGE_GRAB =                  0x0800034B, // (0x14B | ACT_FLAG_STATIONARY | ACT_FLAG_PAUSE_EXIT)
        ACT_LEDGE_CLIMB_SLOW_1 =          0x0000054C, // (0x14C | ACT_FLAG_MOVING)
        ACT_LEDGE_CLIMB_SLOW_2 =          0x0000054D, // (0x14D | ACT_FLAG_MOVING)
        ACT_LEDGE_CLIMB_DOWN =            0x0000054E, // (0x14E | ACT_FLAG_MOVING)
        ACT_LEDGE_CLIMB_FAST =            0x0000054F, // (0x14F | ACT_FLAG_MOVING)
        ACT_GRABBED =                     0x00020370, // (0x170 | ACT_FLAG_STATIONARY | ACT_FLAG_INVULNERABLE)
        ACT_IN_CANNON =                   0x00001371, // (0x171 | ACT_FLAG_STATIONARY | ACT_FLAG_INTANGIBLE)
        ACT_TORNADO_TWIRLING =            0x10020372, // (0x172 | ACT_FLAG_STATIONARY | ACT_FLAG_INVULNERABLE | ACT_FLAG_SWIMMING_OR_FLYING)

        // group 0x180: object actions
        ACT_PUNCHING =                    0x00800380, // (0x180 | ACT_FLAG_STATIONARY | ACT_FLAG_ATTACKING)
        ACT_PICKING_UP =                  0x00000383, // (0x183 | ACT_FLAG_STATIONARY)
        ACT_DIVE_PICKING_UP =             0x00000385, // (0x185 | ACT_FLAG_STATIONARY)
        ACT_STOMACH_SLIDE_STOP =          0x00000386, // (0x186 | ACT_FLAG_STATIONARY)
        ACT_PLACING_DOWN =                0x00000387, // (0x187 | ACT_FLAG_STATIONARY)
        ACT_THROWING =                    0x80000588, // (0x188 | ACT_FLAG_MOVING | ACT_FLAG_THROWING)
        ACT_HEAVY_THROW =                 0x80000589, // (0x189 | ACT_FLAG_MOVING | ACT_FLAG_THROWING)
        ACT_PICKING_UP_BOWSER =           0x00000390, // (0x190 | ACT_FLAG_STATIONARY)
        ACT_HOLDING_BOWSER =              0x00000391, // (0x191 | ACT_FLAG_STATIONARY)
        ACT_RELEASING_BOWSER =            0x00000392, // (0x192 | ACT_FLAG_STATIONARY)
    };
}
