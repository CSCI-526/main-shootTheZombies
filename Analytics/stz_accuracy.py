import pandas as pd
import matplotlib.pyplot as plt

# Load the CSV
df = pd.read_csv("Analytics/stz_accuracy.csv")

# Filter out accuracy values above 1
# df = df[df['accuracy'] <= 1]

# Plotting histogram of accuracy values
plt.figure(figsize=(8, 6))
plt.hist(df['accuracy'], bins=20, edgecolor='black', alpha=0.7)

plt.xlabel("Accuracy")
plt.ylabel("Number of Instances")
plt.title("Histogram of Accuracy Instances (Omitting Values Above 1)")
plt.grid(True)
plt.tight_layout()

plt.show()
