import pandas as pd
import matplotlib.pyplot as plt
from datetime import datetime, timedelta

# Load and clean
df = pd.read_csv("Analytics/stz_zombie_kill_rate.csv")

# Count kills per sessionID
kill_counts = df['sessionID'].value_counts()

# Plot histogram of number of players vs number of kills
plt.figure(figsize=(10, 5))
plt.hist(kill_counts, bins=range(1, kill_counts.max() + 2, 2), edgecolor='black', align='left')

plt.xlabel("Number of Zombies Killed")
plt.ylabel("Number of Players")
plt.title("Distribution of Zombie Kills per Player")
plt.xticks(range(1, kill_counts.max() + 1, 10))
plt.grid(axis='y', linestyle='--', alpha=0.7)
plt.tight_layout()
plt.show()
