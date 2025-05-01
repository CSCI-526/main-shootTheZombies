import pandas as pd
import matplotlib.pyplot as plt
from datetime import datetime, timedelta

# Load data
df = pd.read_csv("Analytics/stz_level_up_rate.csv")

# Convert .NET ticks to datetime
def ticks_to_datetime(tick_str):
    try:
        ticks = int(tick_str)
        return datetime(1, 1, 1) + timedelta(microseconds=ticks // 10)
    except:
        return None

df['sessionStart'] = df['sessionID'].apply(ticks_to_datetime)
df['levelUpTime'] = df['timeLevelUp'].apply(ticks_to_datetime)

# Calculate offset in seconds from session start
df['secondsSinceStart'] = (df['levelUpTime'] - df['sessionStart']).dt.total_seconds()

# Plotting
plt.figure(figsize=(10, 6))

for session_id, group in df.groupby('sessionID'):
    # Sort in case level-ups are unordered
    times = sorted(group['secondsSinceStart'])
    levels = list(range(1, len(times) + 1))  # Level numbers: 1, 2, ...
    plt.plot(times, levels, marker='o', label=f'Player {str(session_id)[-4:]}', alpha=0.7)

plt.xlabel("Seconds Since Session Start")
plt.ylabel("Level")
plt.title("Player Level-Up Timelines (Overlaid)")
plt.grid(True)
plt.tight_layout()

# Optional: omit legend if too many players
if df['sessionID'].nunique() <= 10:
    plt.legend()

plt.show()
