import pandas as pd
import matplotlib.pyplot as plt

# Load the CSV
df = pd.read_csv("Analytics/stz_tower_selection.csv")

# Normalize the type column (just in case there are extra spaces or case differences)
df['typeTowerSelection'] = df['typeTowerSelection'].str.strip().str.lower()

# Categorize into normal, flame, and other
def categorize_tower(tower_type):
    if tower_type == 'normal':
        return 'Normal'
    elif tower_type == 'flame':
        return 'Flame'
    else:
        return 'Other'

df['TowerCategory'] = df['typeTowerSelection'].apply(categorize_tower)

# Count percentages
counts = df['TowerCategory'].value_counts()
percentages = counts / counts.sum() * 100

# Plot
plt.figure(figsize=(6, 6))
plt.pie(percentages, labels=percentages.index, autopct='%1.1f%%', startangle=140, colors=["#4CAF50", "#FF5722", "#9E9E9E"])
plt.title("Tower Selection Distribution")
plt.axis('equal')  # Keep the pie chart circular
plt.tight_layout()
plt.show()
