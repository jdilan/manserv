import re
import base64
from pathlib import Path

# Read the extracted content (you'll need to paste the content here)
# For now, I'll create a script that can extract base64 images

def extract_images_from_markdown(content):
    """Extract base64 encoded images from markdown content"""
    # Pattern to match ![](data:image/png;base64,...)
    pattern = r'!\[\]\(data:image/([^;]+);base64,([^)]+)\)'
    matches = re.findall(pattern, content)
    
    images = []
    for idx, (img_type, base64_data) in enumerate(matches, 1):
        images.append({
            'index': idx,
            'type': img_type,
            'data': base64_data,
            'filename': f'figure_{idx}.{img_type}'
        })
    
    return images

def save_images(images, output_dir='context/processed/images'):
    """Save extracted images to files"""
    Path(output_dir).mkdir(parents=True, exist_ok=True)
    
    for img in images:
        filepath = Path(output_dir) / img['filename']
        img_data = base64.b64decode(img['data'])
        with open(filepath, 'wb') as f:
            f.write(img_data)
        print(f"Saved: {filepath}")

if __name__ == "__main__":
    # This script is ready to extract images
    print("Image extraction script ready")
