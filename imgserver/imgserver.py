
from flask import Flask, request, jsonify, send_from_directory
from flask_cors import CORS
import os

app = Flask(__name__)
CORS(app)  # Cho phép truy cập từ các domain khác nếu cần (CORS)

# Thư mục lưu trữ ảnh
UPLOAD_FOLDER = 'uploads'
os.makedirs(UPLOAD_FOLDER, exist_ok=True)
app.config['UPLOAD_FOLDER'] = UPLOAD_FOLDER

# API: Upload ảnh
@app.route('/upload', methods=['POST'])
def upload_image():
    if 'file' not in request.files:
        return jsonify({'error': 'No file part'}), 400

    file = request.files['file']
    if file.filename == '':
        return jsonify({'error': 'No selected file'}), 400

    # Lưu file với tên gốc (hoặc tùy chỉnh tên)
    file_path = os.path.join(app.config['UPLOAD_FOLDER'], file.filename)
    file.save(file_path)

    # Trả về URL của file đã upload
    return jsonify({'message': 'File uploaded successfully', 'file_url': f'/images/{file.filename}'})

# API: Phục vụ ảnh
@app.route('/images/<filename>', methods=['GET'])
def serve_image(filename):
    return send_from_directory(app.config['UPLOAD_FOLDER'], filename)

# API: Kiểm tra server hoạt động
@app.route('/', methods=['GET'])
def home():
    return jsonify({'message': 'Flask API is running!'})

if __name__ == '__main__':
    app.run(host='0.0.0.0', port=5000, debug=True)
